using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
	[Authorize]
    [Area("account")]
    public class SecurityController : Controller
    {
        private readonly UserManager<User> _userManager;
        public SecurityController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> Index() => View(await _userManager.GetUserAsync(User));

		[HttpPost]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
			var res = new Responce();
			
            if (ModelState.IsValid)
            {
				User user = await _userManager.GetUserAsync(User);
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmPassword);
                if (result.Succeeded)
				{

					user.LastPasswordUpdate = DateTime.Now;
					res.Message = "Пароль изменен";
					res.Success = true;
					res.Data = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
				}
                else
				{
					res.Message = "Введенные данные не совпадают";
					res.Success = false;
				}
            }
			else
			{
				res.Message = "Данные введены некорректно";
				res.Success = false;
			}
            return Json(res);
        }

		[HttpPost]
		public ActionResult LogOut()
		{
			return RedirectToAction("Login", "Auth");
		}

		[HttpPost]
		public async Task<ActionResult> Delete()
		{
			await _userManager.DeleteAsync(await _userManager.GetUserAsync(User));
			return RedirectToAction("Login", "Auth");
		}
    }
}
