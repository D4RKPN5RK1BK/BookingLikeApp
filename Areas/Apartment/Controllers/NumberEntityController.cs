using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
	public class NumberEntityController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public NumberEntityController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index(int id)
		{
			Number model = _context.Numbers.Find(id);
			return View(model);
		}

		public IActionResult Read(int id)
		{
			NumberEntityViewModel model = new NumberEntityViewModel();
			model.Dates = new List<DateTime>();
			DateTime now = DateTime.Now;
			for(int i = 0; i < 28; i++)
			{
				model.Dates.Add(now.Subtract(new TimeSpan(14)));
			}
			return View();
		}
	}
}
