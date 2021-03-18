using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
	public class ReadController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<User> _userManager;

		public ReadController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			User user = await _userManager.GetUserAsync(User);
			List<Models.Apartment> model = _context.Apartments.Where(o => o.UserId == user.Id).ToList();

			for(int i = 0; i < model.Count; i++)
			{
				model[i].Numbers = _context.Numbers.Where(o => o.ApartmentId == model[i].Id).ToList();
			}
			return View(_context.Apartments.ToList());
		}

		public IActionResult SearchByName (string name)
		{
			if (_context.Apartments.Any(o => o.Name == name))
				return View(_context.Apartments.Where(o => o.Name == name).ToList());
			return View("Search", _context.Apartments.Where(o => o.Name.Contains(name)).ToList(), new { Name = name });
		}

		public async Task<ActionResult> Search(SearchViewModel model)
		{
			return View("Search");
		}

		public IActionResult Details(int id)
		{
			Models.Apartment apartment = _context.Apartments.Find(id);
			apartment.Numbers = _context.Numbers.Where(o => o.Id == apartment.Id).ToList();
			return View(apartment);
		}
	}
}
