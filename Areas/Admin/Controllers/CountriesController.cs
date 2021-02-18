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
	[Area("admin")]
	public class CountriesController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly IWebHostEnvironment _env;

		//Добавление изображения
		protected async Task<string> AddImageAsync(IFormFile file)
		{
			if (file != null)
			{
				string path = "/images/" + file.FileName;

				using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				return path;
			}

			return String.Empty;
		}

		public CountriesController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

		public IActionResult Index()
		{
			return View(_context.Countries.ToList());
		}

		public async Task<IActionResult> Edit(int id) => View(await _context.Countries.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(Country model)
		{
			if (ModelState.IsValid)
			{
				await AddImageAsync(model.PhotoFile);
				await AddImageAsync(model.FlagFile);
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Countries.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(Country model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		public IActionResult Create(int id) => View(new Country());
		[HttpPost]
		public async Task<IActionResult> Create(Country model)
		{
			if (ModelState.IsValid)
			{
				await AddImageAsync(model.PhotoFile);
				await AddImageAsync(model.FlagFile);
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
