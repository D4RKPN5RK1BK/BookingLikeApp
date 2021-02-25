using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
	public class ReadController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public ReadController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_context.Apartments.ToList());
		}

		public IActionResult Search (string name)
		{
			if (_context.Apartments.Any(o => o.Name == name))
				return View(_context.Apartments.Where(o => o.Name == name).ToList());
			return View(_context.Apartments.Where(o => o.Name.Contains(name)).ToList());
		}

		public IActionResult Details(int id)
		{
			Models.Apartment apartment = _context.Apartments.Find(id);
			apartment.Numbers = _context.Numbers.Where(o => o.Id == apartment.Id).ToList();
			return View(apartment);
		}
	}
}
