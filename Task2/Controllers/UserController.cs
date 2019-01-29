using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using News_portal.Models;

namespace News_portal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user.Email == User.Identity.Name)
            {
                await _signInManager.SignOutAsync();
            }
            await _userManager.DeleteAsync(user);
            return RedirectPermanent("/User/UserList");
        }

        [Authorize]
        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.LockoutEnabled = false;
            if (user.Email == User.Identity.Name)
            {
                await _signInManager.SignOutAsync();
            }
            await _userManager.UpdateAsync(user);
            return RedirectPermanent("/User/UserList");
        }

        [Authorize]
        public async Task<IActionResult> UnLockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.LockoutEnabled = true;
            if (user.Email == User.Identity.Name)
            {
                await _signInManager.SignOutAsync();
            }
            await _userManager.UpdateAsync(user);
            return RedirectPermanent("/User/UserList");
        }

        [Authorize(Roles = "admin")]
        public IActionResult UserList()
        {
            var users = _userManager.Users;
            return View(users);
        }
    }
}