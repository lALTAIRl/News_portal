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
        public async Task<IActionResult> CreateNews(NewsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var news = _mapper.Map<News>(model);
                news.DateOfCreating = DateTime.Now;
                await _newsRepository.CreateAsync(news);
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

        public async Task<IActionResult> ViewNews(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            var model = _mapper.Map<NewsViewModel>(news);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PublishNews(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            news.IsPublished = true;
            news.DateOfPublishing = DateTime.Now;
            await _newsRepository.UpdateAsync(news);
            return RedirectToAction("NewsManagement");
        }

        [HttpPost]
        public async Task<IActionResult> UnpublishNews(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            news.IsPublished = false;
            await _newsRepository.UpdateAsync(news);
            return RedirectToAction("NewsManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditNews(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            var model = _mapper.Map<NewsEditViewModel>(news);
            return View(model);
        }

        public async Task<IActionResult> UpdateNews(NewsEditViewModel newsEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var news = await _newsRepository.GetByIdAsync(newsEditViewModel.Id);
                _mapper.Map(newsEditViewModel, news);
                await _newsRepository.UpdateAsync(news);
                return RedirectToAction("NewsManagement");
            }
            else
            {
                return RedirectToAction("EditNews", newsEditViewModel);
            }
        }

        public async Task<IActionResult> DeleteNews(News news)
        {
            await _newsRepository.DeleteAsync(news);
            return RedirectToAction("NewsManagement");
        }

        public async Task<IActionResult> NewsCollection(int page = 1)
        {
            var news = await _newsRepository.GetAllAsync();
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

        public async Task<IActionResult> NewsManagement(int page = 1)
        {
            var news = await _newsRepository.GetAllAsync();
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
            var news = await _newsRepository.GetByIdAsync(id);
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

        public async Task<IActionResult> ViewFavourites(int id)
        {
            var userId = _userManager.GetUserId(User);
            var news = await _newsRepository.GetUsersFavouritesAsync(userId);
            return View(news);
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