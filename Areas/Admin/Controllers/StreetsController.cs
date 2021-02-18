using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StreetsController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public StreetsController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
		}

		public IActionResult Index() => View(_context.Streets.ToList());

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.CityList = _context.Cities.ToList();
			return View(await _context.Streets.FindAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Street model)
		{
			if (ModelState.IsValid)
			{
				model.CityId = _context.Cities.Where(o => o.Name == model.CityName).FirstOrDefault().Id;
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Streets.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(Street model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		public IActionResult Create(int id)
		{
			ViewBag.CityList = _context.Cities.ToList();
			return View(new Street());
		}
		[HttpPost]
		public async Task<IActionResult> Create(Street model)
		{
			if (ModelState.IsValid)
			{
				model.CityId = _context.Cities.Where(o => o.Name == model.CityName).FirstOrDefault().Id;
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
