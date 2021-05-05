using BookingLikeApp.Areas.Account.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		[HttpPut]
		public async Task<ActionResult> Avatar(IFormFile file)
		{
			if(file != null && file.Length > 0)
			{
				User user = await _userManager.GetUserAsync(User);
				string path = "/images/" + user.Id.GetHashCode() + Path.GetExtension(file.FileName);

				using (var fs = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(fs);
				}

				user.PFPUrl = path;
				_context.Update(user);
				await _context.SaveChangesAsync();
				return Json(new { Exception = string.Empty, Success = true });
			}
			return View("Index");
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
