using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Data;
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
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login() => View(); 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            User user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError(String.Empty, "Login or password is incorrect");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = ""}); 
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Login or password is incorrect");
                }

            }
            return View(model);
        }

        public IActionResult Register() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    LockoutEnabled = true,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
