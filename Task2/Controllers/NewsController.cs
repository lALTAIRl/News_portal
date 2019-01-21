using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using News_portal.Data;
using Microsoft.EntityFrameworkCore;
using News_portal.Models;
using News_portal.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using News_portal.Interfaces;
using News_portal.Repositories;

namespace News_portal.Controllers
{
    public class NewsController : Controller
    {
        private readonly IMapper _mapper;
        ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        public NewsController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
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
                _context.NewsCollection.Add(news);
                _context.SaveChanges();
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
            var news = _context.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsViewModel>(news);
            return View(model);
        }    

        [HttpPost]
        public async Task<IActionResult> PublishNews(int id)
        {
            var news = await _context.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = true;
            news.DateOfPublishing = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UnpublishNews(int id)
        {
            var news = await _context.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            news.IsPublished = false;
            _context.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var news = _context.NewsCollection.FirstOrDefault(m => m.Id == id);
            var model = _mapper.Map<NewsEditViewModel>(news);
            return View(model);
        }

        [HttpPost]
        public IActionResult ApplyNewsEditing(NewsEditViewModel newsEditViewModel)
        {
            if(ModelState.IsValid)
            {
                var news = _context.NewsCollection.FirstOrDefault(m => m.Id == newsEditViewModel.Id);
                _mapper.Map(newsEditViewModel, news);
                _context.NewsCollection.Update(news);
                _context.SaveChanges();
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
            var news = await _context.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            _context.NewsCollection.Remove(news);
            _context.SaveChanges();
            return RedirectToAction("NewsManagement");
        }

        public async Task<IActionResult> NewsCollection(int page=1)
        {
            IQueryable<News> news = _context.NewsCollection;
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
            IQueryable<News> news = _context.NewsCollection;
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
            var news = await _context.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            var userId = _userManager.GetUserId(User);
            var favouriteNews = new NewsApplicationUser
            {
                NewsId = news.Id,
                FavouriteNews = news,
                ApplicationUserId = userId,
                ApplicationUserFavourited = await _userManager.GetUserAsync(User)
            };
            if(await _context.FindAsync<NewsApplicationUser>(id, userId)==null)
            {
                _context.Add(favouriteNews);
                _context.SaveChanges();
            }
            return RedirectToAction("NewsCollection");
        }

        public async Task<IActionResult> ViewFavourites(int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            var favoriteNews = _context.NewsCollection;
            var model= new List<News>();
            foreach (var news in favoriteNews)
            {            
                if(await _context.FindAsync<NewsApplicationUser>(news.Id, userId)!=null)
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
            var favouriteNews = await _context.FindAsync<NewsApplicationUser>(id, userId);
            if(favouriteNews!=null)
            {
                _context.Remove(favouriteNews);
                _context.SaveChanges();
            }           
            return RedirectToAction("ViewFavourites");
        }

    }

}