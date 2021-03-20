using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	public class NumberServiceController : Controller
	{

		protected readonly ApplicationDbContext _context;

		public NumberServiceController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<ActionResult> Create(int numberId)
		{
			NumberService model = new NumberService()
			{
				NumberId = numberId
			};

			return Json(model);
		}
	}
}
