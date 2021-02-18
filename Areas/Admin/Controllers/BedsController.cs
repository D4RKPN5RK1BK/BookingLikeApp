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
	public class BedsController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public BedsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index() => View(_context.Beds.ToList());

		public async Task<IActionResult> Edit(int id) => View(await _context.Beds.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(Bed model)
		{
			if (ModelState.IsValid)
			{
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Beds.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(Bed model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		public IActionResult Create() => View(new Bed());
		[HttpPost]
		public async Task<IActionResult> Create(Bed model)
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
