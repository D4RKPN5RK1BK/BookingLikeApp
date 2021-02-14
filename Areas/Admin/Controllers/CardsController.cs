using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class CardsController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> Edit()
		{
			return View();
		}

		public async Task<IActionResult> Delete()
		{
			return View();
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}
	}
}
