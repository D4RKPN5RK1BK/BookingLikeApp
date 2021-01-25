using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
    [Area("account")]
    public class SecurityController : Controller
    {
        private readonly UserManager<User> _userManager;
        public SecurityController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword() => View();
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmPassword);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Security");
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                
            }
            return View(model);
        }
    }
}
