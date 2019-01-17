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
        ApplicationDbContext _contex;
        UserManager<ApplicationUser> _userManager;
        public NewsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _contex = context;
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
                _contex.NewsCollection.Add(news);
                _contex.SaveChanges();
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
            var news = _contex.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsViewModel>(news);
            return View(model);
        }    

        [HttpPost]
        public async Task<IActionResult> PublishNews(int id)
        {
            var news = await _contex.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = true;
            news.DateOfPublishing = DateTime.Now;
            _contex.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UnpublishNews(int id)
        {
            var news = await _contex.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = false;
            _contex.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var news = _contex.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsEditViewModel>(news);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyNewsEditing(NewsEditViewModel newsEditViewModel)
        {
            if(ModelState.IsValid)
            {
                var news = _contex.NewsCollection.FirstOrDefault(m => m.Id == newsEditViewModel.Id);
                _mapper.Map(newsEditViewModel, news);
                _contex.NewsCollection.Update(news);
                _contex.SaveChanges();
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
            var news = await _contex.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            _contex.NewsCollection.Remove(news);
            _contex.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        public async Task<IActionResult> NewsCollection(int page=1)
        {
            IQueryable<News> news = _contex.NewsCollection;
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
            IQueryable<News> news = _contex.NewsCollection;
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
            var news = await _contex.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            var userId = _userManager.GetUserId(User);
            var favouriteNews = new NewsApplicationUser
            {
                NewsId = news.Id,
                FavouriteNews = news,
                ApplicationUserId = userId,
                ApplicationUserFavourited = await _userManager.GetUserAsync(User)
            };
            if(await _contex.FindAsync<NewsApplicationUser>(id, userId)==null)
            {
                _contex.Add(favouriteNews);
                _contex.SaveChanges();
            }
            return RedirectToAction("NewsCollection");
        }

        public async Task<IActionResult> ViewFavourites(int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            var favoriteNews = _contex.NewsCollection;
            var model= new List<News>();
            foreach (var news in favoriteNews)
            {            
                if(await _contex.FindAsync<NewsApplicationUser>(news.Id, userId)!=null)
                {
                    model.Add(news);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavourites(int id)
        {
            var userId = _userManager.GetUserId(User);
            var favouriteNews = await _contex.FindAsync<NewsApplicationUser>(id, userId);
            if(favouriteNews!=null)
            {
                _contex.Remove(favouriteNews);
                _contex.SaveChanges();
            }           
            return RedirectToAction("ViewFavourites");
        }

    }

}