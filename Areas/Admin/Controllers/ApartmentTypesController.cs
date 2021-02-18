using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "admin")]
	public class ApartmentTypesController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly IWebHostEnvironment _env;

		protected async Task<string> AddImageAsync(IFormFile file)
		{
			if (file != null)
			{
				string path = "/images/" + file.FileName;

				using(var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				return path;
			}

			return String.Empty;
		}

		public ApartmentTypesController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_env = env;
			_context = context;
		}

		public IActionResult Index() => View(_context.ApartmentTypes.ToList());

		public async Task<IActionResult> Edit(int? id) => View(await _context.ApartmentTypes.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(ApartmentType model)
		{
			if (ModelState.IsValid)
			{
				model.PhotoUrl = await AddImageAsync(model.File);
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int? id) => View(await _context.ApartmentTypes.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(ApartmentType model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public IActionResult Create() => View(new ApartmentType());
		[HttpPost]
		public async Task<IActionResult> Create(ApartmentType model)
		{
			if (ModelState.IsValid)
			{
				model.PhotoUrl = await AddImageAsync(model.File);
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
