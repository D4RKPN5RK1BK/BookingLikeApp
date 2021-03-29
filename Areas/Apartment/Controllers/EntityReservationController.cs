using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	public class EntityReservationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Read(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			return View();
		}


	}
}
