using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class RoomsController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public RoomsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index() => View(_context.Rooms.ToList());

		public async Task<IActionResult> Edit(int id) => View(await _context.Rooms.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(Room model)
		{
			if (ModelState.IsValid)
			{
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Rooms.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(Room model)
		{
			if (ModelState.IsValid)
			{
				_context.Remove(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Create() => View(new Room());
		[HttpPost]
		public async Task<IActionResult> Create(Room model)
		{
			if (ModelState.IsValid)
			{
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
