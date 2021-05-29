using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
    [Area("account")]
    public class AuthController : Controller
    {
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
		protected readonly IEmailService _emailService;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
			_emailService = emailService;
        }

        public IActionResult Login() => View(); 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            User user = await _userManager.FindByNameAsync(model.UserName);

			var result = await _signInManager.PasswordSignInAsync(user ?? new Models.User(), model.Password, model.RememberMe, false);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home", new { area = "" });
			}
			else
			{
				ModelState.AddModelError(String.Empty, "Пароль или логин не совпадают");
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
                    LockoutEnabled = true,
					RegistationDate = DateTime.Now,
					DisplayName = model.UserName,
					LastPasswordUpdate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
					var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

					if (signInResult.Succeeded)
					{
						return RedirectToAction("Index", "Home", new { area = "" });
					}
					else
					{
						return BadRequest();
					}
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
