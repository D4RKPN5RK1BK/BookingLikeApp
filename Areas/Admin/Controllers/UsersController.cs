using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class UsersController : Controller
	{
		protected readonly ApplicationDbContext _context;
		public UsersController( ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View(_context.Users.ToList());
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
