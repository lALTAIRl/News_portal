using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Microsoft.EntityFrameworkCore;
using Task2.Models;
using Task2.ViewModels;
using AutoMapper;

namespace Task2.Controllers
{
    public class NewsController : Controller
    {
        private readonly IMapper _mapper;
        ApplicationDbContext db;
        public NewsController(ApplicationDbContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNews(NewsCreateViewModel model)
        {
            var news = _mapper.Map<News>(model);
            news.DateOfCreating = DateTime.Now;
            db.NewsCollection.Add(news);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
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
            var news = db.NewsCollection.FirstOrDefault(m => m.Id == newsEditViewModel.Id);
            _mapper.Map<News>(newsEditViewModel);
            db.NewsCollection.Update(news);
            db.SaveChanges();
            return RedirectToAction("NewsManagement");
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

    }

}