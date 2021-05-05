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
                    LockoutEnabled = true,
					RegistationDate = DateTime.Now,
					DisplayName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
					//было
					var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

					if (signInResult.Succeeded)
					{
						return RedirectToAction("Index", "Home", new { area = "" });
					}
					else
					{
						return BadRequest();
					}

					//стало (с отправкой на письма на почту)

					/*var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var link = Url.Action(nameof(VerifyEmail), "Auth", new { userId = user.Id, code });

					var email = new MimeMessage();
					email.From.Add(MailboxAddress.Parse("BookingLIkeApp@ya.ru"));
					email.To.Add(MailboxAddress.Parse(model.Email));
					email.Subject = "Подтверждение почты";
					email.Body = new TextPart(TextFormat.Html) { Text = $"<a href=\"{link}\"'>Подтвердить почту</a>" };

					using (var smtp = new SmtpClient())
					{
						smtp.Connect("smtp.yandex.ru", 465, true);
						smtp.Authenticate("BookingLikeApp@ya.ru", "ajbcgcasabhzacvl");
						smtp.Send(email);
						smtp.Disconnect(true);
					}


					return RedirectToAction("VerifyEmail");*/

				}
				else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                }

            }
            return View(model);
        }

		public async Task<ActionResult> VerifyEmail(string userId, string code)
		{
			return View();
		}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
