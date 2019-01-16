using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Microsoft.EntityFrameworkCore;
using Task2.Models;
using Task2.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Task2.Controllers
{
    public class NewsController : Controller
    {
        private readonly IMapper _mapper;
        ApplicationDbContext db;
        UserManager<ApplicationUser> _userManager;
        public NewsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            db = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNews(NewsCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var news = _mapper.Map<News>(model);
                news.DateOfCreating = DateTime.Now;
                db.NewsCollection.Add(news);
                db.SaveChanges();
                return RedirectToAction("NewsCollection");
            }

            else
            {
                if (string.IsNullOrEmpty(model.Text))
                {
                    ModelState.AddModelError("", "Text required");
                }
                return View(model);
            }
        }

        public IActionResult ViewNews(int id)
        {
            var news = db.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsViewModel>(news);
            return View(model);
        }    

        [HttpPost]
        public async Task<IActionResult> PublishNews(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = true;
            news.DateOfPublishing = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UnpublishNews(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = false;
            db.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var news = db.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsEditViewModel>(news);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyNewsEditing(NewsEditViewModel newsEditViewModel)
        {
            if(ModelState.IsValid)
            {
                var news = db.NewsCollection.FirstOrDefault(m => m.Id == newsEditViewModel.Id);
                //Mapper.Initialize(cfg => cfg.CreateMap<NewsEditViewModel, News>());
                //Mapper.Map(newsEditViewModel, news);

                //var news = _mapper.Map<News>(newsEditViewModel);

                _mapper.Map(newsEditViewModel, news);

                db.NewsCollection.Update(news);
                db.SaveChanges();
                return RedirectToAction("NewsManagement");
            }
            else
            {
                return RedirectToAction("EditNews", newsEditViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            db.NewsCollection.Remove(news);
            db.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        public async Task<IActionResult> NewsCollection(int page=1)
        {
            IQueryable<News> news = db.NewsCollection;
            news = news.OrderByDescending(s => s.DateOfCreating);
            int pageSize = 5;
            var count = await news.CountAsync();
            var items = await news.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PageIndexViewModel viewModel = new PageIndexViewModel
            {
                PageViewModel = pageViewModel,
                EnumNews = items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> NewsManagement(int page = 1)
        {
            IQueryable<News> news = db.NewsCollection;
            news = news.OrderByDescending(s => s.DateOfCreating);
            int pageSize = 10;
            var count = await news.CountAsync();
            var items = await news.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PageIndexViewModel viewModel = new PageIndexViewModel
            {
                PageViewModel = pageViewModel,
                EnumNews = items
            };
            return View(viewModel);
        }




        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavourites(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);

            var favouriteNews = new NewsApplicationUser
            {
                NewsId = news.Id,
                FavouriteNews = news,
                ApplicationUserId = _userManager.GetUserId(User),
                ApplicationUserFavourited = await _userManager.GetUserAsync(User)
            };

            db.FavouriteNews.Add(favouriteNews);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

        public IActionResult ViewFavourites(int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            var favNews = db.FavouriteNews.Where(item => item.ApplicationUserId == userId).ToList();
            return View(favNews);
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromFavourites(int newsId)
        {
            var userId = _userManager.GetUserId(User);
            var favouriteNews = db.FavouriteNews.Where(item => item.ApplicationUserId == userId).Where(item => item.NewsId == newsId).FirstOrDefault();

            db.FavouriteNews.Remove(favouriteNews);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

    }

}