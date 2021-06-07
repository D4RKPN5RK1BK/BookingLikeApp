using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("Apartment")]
	public class CreateController : Controller
	{

		private readonly UserManager<User> _userManager;
		private readonly ApplicationDbContext _context;

		public CreateController(UserManager<User> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<ActionResult> Index()
		{
			Models.Apartment apartment = await _context.Apartments
				.FirstOrDefaultAsync(o => o.Finished == false && o.UserId == _userManager.GetUserId(User));

			if (apartment != null)
			{
				ViewData["Exist"] = true;
				ViewData["Id"] = apartment.Id;
			}
			else
				ViewData["Exist"] = false;

			ViewBag.ApartmentTypes = _context.ApartmentTypes.ToList();
			return View();
		}

		public async Task<ActionResult> Initialize(int apartmentType)
		{
			Models.Apartment apartment = await _context.Apartments
				.FirstOrDefaultAsync(o => !o.Finished && o.UserId == _userManager.GetUserId(User));

			if (apartment != null)
				_context.Remove(apartment);

			apartment = new Models.Apartment()
			{
				UserId = _userManager.GetUserId(User),
				ApartmentTypeId = apartmentType,
				Name = _context.ApartmentTypes.Find(apartmentType).Name,
				Enable = false,
				Finished = false,
				Bolcked = false,
				Checked = false,
				CreateTimeStamp = DateTime.Now
			};
			apartment.Registration = new Registration();
			await _context.AddAsync(apartment);
			await _context.SaveChangesAsync();

			return RedirectToAction("BasicInfo", "Edit", new { apartment.Id });
		}


	}
}

