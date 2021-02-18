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
	public class CitiesController : Controller
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

		public CitiesController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

		public IActionResult Index()
		{
			return View(_context.Cities.ToList());
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.CountryList = _context.Countries.ToList();
			return View(await _context.Cities.FindAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Edit(City model)
		{
			if (ModelState.IsValid)
			{
				await AddImageAsync(model.File);
				model.CountryId = _context.Countries.Where(o => o.Name == model.CountryName).FirstOrDefault().Id;
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Cities.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(City model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		public IActionResult Create(int id)
		{
			ViewBag.CountryList = _context.Countries.ToList();
			return View(new City());
		}
		[HttpPost]
		public async Task<IActionResult> Create(City model)
		{
			if (ModelState.IsValid)
			{
				await AddImageAsync(model.File);

				model.CountryId = _context.Countries.Where(o => o.Name == model.CountryName).FirstOrDefault().Id;

				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
