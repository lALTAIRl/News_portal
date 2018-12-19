﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Microsoft.EntityFrameworkCore;
using Task2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Task2.Models;
using Task2.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Globalization;


namespace Task2.Controllers
{
    public class NewsController : ManageController
    {
        private readonly UserManager<ApplicationUser> _ApplicationUserManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        ApplicationDbContext db;

        public NewsController(UserManager<ApplicationUser> ApplicationUserManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, ILogger<ManageController> logger, UrlEncoder urlEncoder, ApplicationDbContext context) : base(ApplicationUserManager, signInManager, roleManager, emailSender, logger, urlEncoder, context)
        {
            _ApplicationUserManager = ApplicationUserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            db = context;
        }


        

        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNews(string caption, string text, string imageurl, DateTime dateofcreating)
        {
            News news = new News(caption,  text, imageurl, dateofcreating);
            
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

        //public async Task<IActionResult> EditNewsAsync(int id)
        //{
        //    var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
        //    return View(new EditNewsViewModel(id));
        //}


        public IActionResult EditNews(int id)
        {
            //var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            return View(new EditNewsViewModel());
        }

        public async Task<IActionResult> ApplyNewsEditing(int id)
        {
            var news = await db.NewsCollection.SingleOrDefaultAsync(m => m.Id == id);
            db.SaveChanges();
            return RedirectToAction("NewsCollection");
        }

        public IActionResult NewsCollection(SortState sortOrder = SortState.DateOfCreatingDescendingly)
        {
            IQueryable<News> news = db.NewsCollection;
            ViewData["DateOfCreating"] = sortOrder == SortState.DateOfCreatingAscending ?
                SortState.DateOfCreatingDescendingly : SortState.DateOfCreatingAscending;

            switch(sortOrder)
            {
                case SortState.DateOfCreatingDescendingly:
                    news = news.OrderByDescending(s => s.DateOfCreating);
                    break;
                default:
                    news = news.OrderBy(s => s.DateOfCreating);
                    break;
            }

            return View(news);
        }

    }
}