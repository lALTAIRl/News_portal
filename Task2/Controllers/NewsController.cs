using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Microsoft.EntityFrameworkCore;
using Task2.Models;
using Task2.ViewModels;

namespace Task2.Controllers
{
    public class NewsController : Controller
    {
        ApplicationDbContext db;
        public NewsController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNews(string caption, string text, string imageUrl, DateTime dateOfCreating, bool isPublished, DateTime dateOfPublishing)
        {
            var news = new News(caption,  text, imageUrl, dateOfCreating, isPublished, dateOfPublishing);
            
            db.NewsCollection.Add(news);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            db.NewsCollection.Remove(news);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

        //public IActionResult EditNews(int id)
        //{
        //    News post=db.NewsCollection.FirstOrDefault(m => m.Id == id);
        //    return View(new ViewNewsViewModel(post));
        //}

        public IActionResult ViewNews(int id)
        {
            var post = db.NewsCollection.FirstOrDefault(m => m.Id == id);
            return View(post);
        }

        public async Task<IActionResult> ApplyNewsEditing(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

        //public IActionResult NewsCollection()
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

        [HttpPost]
        public IActionResult CreateFirstNews()
        {
            if (db.NewsCollection.FirstOrDefault() == null)
            {
                var news = new News
                {
                    Caption = "test news",
                    Text = "<b>first test news<b>",
                    ImageURL = "https://dpchas.com.ua/sites/default/files/u85/22_27.jpg",
                    DateOfCreating = DateTime.Now,
                    IsPublished = true,
                    DateOfPublishing = DateTime.Now
                };

                db.NewsCollection.Add(news);
                db.SaveChanges();
            }
            return RedirectToAction("NewsCollection");
        }

    }

}