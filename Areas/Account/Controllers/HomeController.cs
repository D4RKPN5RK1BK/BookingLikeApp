using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
	[Authorize]
    [Area("account")]
    public class HomeController : Controller
    {
        protected readonly UserManager<User> _userManager;
		protected readonly IWebHostEnvironment _env;
		protected readonly ApplicationDbContext _context;

		protected async Task<string> AddImageAsync(IFormFile file)
		{
			if (file != null)
			{
				var path = file.FileName;

				using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				return path;
			}

			return String.Empty;
		}

		public HomeController(UserManager<User> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
			_env = env;
			_context = context;
        }

        public async Task<IActionResult> Index()
        {
            User model = await _userManager.GetUserAsync(User);
            return View(model);
        }

		[HttpPost]
		public async Task GenderUpdate([FromBody]bool? gender)
		{
			User user = await _userManager.GetUserAsync(User);
			user.Gender = gender;

			_context.Update(user).Property(o => o.Gender);
			await _context.SaveChangesAsync();
		}

		[HttpPost]
		public async Task DisplayNameUpdate([FromBody]string displayName)
		{
			User user = await _userManager.GetUserAsync(User);
			user.DisplayName = displayName;

			_context.Update(user).Property(o => o.DisplayName);
			await _context.SaveChangesAsync();
		}

		[HttpPost]
		public async Task BirthDateUpdate([FromBody] DateTime birthDate)
		{
			User user = await _userManager.GetUserAsync(User);
			user.DateOfBirth = birthDate;

			_context.Update(user).Property(o => o.DateOfBirth);
			await _context.SaveChangesAsync();
		}




		[HttpPost]
		public async Task<ActionResult> Data([FromBody]UserDataViewModel model)
		{
			User user = await _userManager.GetUserAsync(User);

			if (ModelState.IsValid)
			{
				user.DisplayName = model.DisplayName;
				user.DateOfBirth = model.BirthDate;
				user.Gender = model.Gender;

				_context.Update(user);
				await _context.SaveChangesAsync();

			}
			else
			{
				if (string.IsNullOrEmpty(model.DisplayName))
					return Json(new { Message = "Данное поле является обязательным для заполнения", Target = "display_name", Success = false });

				return Json(new { Message = "Ошибка в оформлении запроса", Target = "", Success = false });
			}

			return Json(new { Message = "Данные успешно обновлены", Target = "", Success = true });
		}

		public async Task<ActionResult> UpdateImage(IFormFile file)
		{
			ProfileImageViewModel model = new ProfileImageViewModel(size: 250);
			User user = await _userManager.GetUserAsync(User);
			string path = await AddImageAsync(file);

			user.PFPUrl = path;
			model.Path = path;
			_context.Update(user);
			await _context.SaveChangesAsync();
			return PartialView("_ProfileImage", model);
		}

		[HttpDelete]
		public async Task<ActionResult> DeleteImage()
		{
			User user = await _userManager.GetUserAsync(User);
			user.PFPUrl = null;
			_context.Update(user);
			var result = await _context.SaveChangesAsync(); 
			Responce res = new Responce();
			res.Message = "Изображение удалено";
			res.Success = true;
			return Json(res);
		}



		/*[HttpPost]
		public ActionResult SendEmail(string model)
		{
			return Json(new { });
		}

		[HttpPost]
		public ActionResult ConfirmEmail(string model)
		{
			return Json(new { });
		}*/
	}
}
