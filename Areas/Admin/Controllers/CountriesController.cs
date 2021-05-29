using System;
using System.IO;
using System.Linq;
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
	[Authorize(Roles = "staff")]
	public class CountriesController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly IWebHostEnvironment _env;

		//Добавление изображения
		protected async Task<string> AddImageAsync(IFormFile file)
		{
			if (file != null)
			{
				string path = "/images/" + Guid.NewGuid() + file.FileName;

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

		[HttpGet]
		public ActionResult Index() => View(_context.Countries.ToList());

		[HttpGet]
		public async Task<IActionResult> Edit(int id) => View(await _context.Countries.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(Country model)
		{
			if (ModelState.IsValid)
			{
				if (model.File != null)
					model.PhotoUrl = await AddImageAsync(model.File);
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int id) => View(await _context.Countries.FindAsync(id));
		[HttpPost]
		public async Task<ActionResult> Delete(Country model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public ActionResult Create() => View(new Country());
		[HttpPost]
		public async Task<ActionResult> Create(Country model)
		{
			if (ModelState.IsValid)
			{
				if (model.File != null)
					model.PhotoUrl = await AddImageAsync(model.File);
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
