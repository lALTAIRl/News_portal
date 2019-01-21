using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News_portal.Interfaces;
using News_portal.Models;
using News_portal.ViewModels;

namespace News_portal.Controllers
{
    public class INewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        UserManager<ApplicationUser> _userManager;

        public INewsController(INewsRepository newsRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Create()
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
                _newsRepository.Create(news);
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
            var news = _newsRepository.GetNewsById(id);
            var model = _mapper.Map<NewsViewModel>(news);
            return View(model);
        }

        [HttpPost]
        public IActionResult PublishNews(int id)
        {
            var news = _newsRepository.GetNewsById(id);
            news.IsPublished = true;
            news.DateOfPublishing = DateTime.Now;
            _newsRepository.Update(news);
            return RedirectToAction("NewsManagement");
        }

        [HttpPost]
        public IActionResult UnpublishNews(int id)
        {
            var news = _newsRepository.GetNewsById(id);
            news.IsPublished = false;
            _newsRepository.Update(news);
            return RedirectToAction("NewsManagement");
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var news = _newsRepository.GetNewsById(id);
            var model = _mapper.Map<NewsEditViewModel>(news);
            return View(model);
        }

        public IActionResult UpdateNews(NewsEditViewModel newsEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var news = _newsRepository.GetNewsById(newsEditViewModel.Id);
                _mapper.Map(newsEditViewModel, news);
                _newsRepository.Update(news);
                return RedirectToAction("NewsManagement");
            }
            else
            {
                return RedirectToAction("EditNews", newsEditViewModel);
            }
        }

        public IActionResult DeleteNews(News news)
        {
            _newsRepository.Delete(news);
            return RedirectToAction("NewsManagement");
        }

        public IActionResult NewsCollection(int page = 1)
        {
            var news = _newsRepository.GetAllNews();
            news.OrderByDescending(s => s.DateOfCreating);
            int pageSize = 5;
            var count = news.Count();
            var items = news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PageIndexViewModel viewModel = new PageIndexViewModel
            {
                PageViewModel = pageViewModel,
                EnumNews = items
            };
            return View(viewModel);

        }

        public IActionResult NewsManagement(int page = 1)
        {
            var news = _newsRepository.GetAllNews();
            news = news.OrderByDescending(s => s.DateOfCreating);
            int pageSize = 10;
            var count = news.Count();
            var items = news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
            var news = _newsRepository.GetNewsById(id);
            var userId = _userManager.GetUserId(User);
            var favouriteNews = new NewsApplicationUser
            {
                NewsId = news.Id,
                FavouriteNews = news,
                ApplicationUserId = userId,
                ApplicationUserFavourited = await _userManager.GetUserAsync(User)
            };
            if (await _context.FindAsync<NewsApplicationUser>(id, userId) == null)
            {
                _context.Add(favouriteNews);
                _context.SaveChanges();
            }
            return RedirectToAction("NewsCollection");
        }

        public async Task<IActionResult> ViewFavourites(int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            var favoriteNews = _newsRepository.GetAllNews();
            var model = new List<News>();
            foreach (var news in favoriteNews)
            {
                if (await _context.FindAsync<NewsApplicationUser>(news.Id, userId) != null)
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
            if (favouriteNews != null)
            {
                _context.Remove(favouriteNews);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewFavourites");
        }

    }
}