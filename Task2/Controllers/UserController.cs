using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Microsoft.AspNetCore.Identity;
using Task2.Services;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Task2.Data;

namespace Task2.Controllers
{
    public class UserController : Controller
    {
        //private readonly UserManager<ApplicationUser> _ApplicationUserManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailSender _emailSender;
        //private readonly ILogger _logger;
        //private readonly UrlEncoder _urlEncoder;

        //private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        //private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        //ApplicationDbContext db;

        //public UserController(UserManager<ApplicationUser> ApplicationUserManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, ILogger<ManageController> logger, UrlEncoder urlEncoder, ApplicationDbContext context) : base(ApplicationUserManager, signInManager, roleManager, emailSender, logger, urlEncoder, context)
        //{
        //    _ApplicationUserManager = ApplicationUserManager;
        //    _signInManager = signInManager;
        //    _roleManager = roleManager;
        //    _emailSender = emailSender;
        //    _logger = logger;
        //    _urlEncoder = urlEncoder;
        //    db = context;
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FavoritesList()
        {
            return View();
        }

        //public async Task<IActionResult> AddToFavoritesList(/*   */)
        //{
        //    ////await
        //    ////
        //    db.SaveChanges();
        //    return RedirectToAction("FavoritesList");
        //}
    }
}