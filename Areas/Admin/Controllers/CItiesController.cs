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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "staff")]
	public class CitiesController : Controller
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

		public CitiesController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

		[HttpGet]
		public IActionResult Index() => View(_context.Cities.ToList());

		[HttpGet]
		public async Task<ActionResult> Edit(int id)
		{
			City model = await _context.Cities.FindAsync(id);
			ViewData["Countries"] = new SelectList(_context.Countries.ToList(), "Id", "Name", model.CountryId);
			return View(model);
		}
		[HttpPost]
		public async Task<ActionResult> Edit(City model)
		{
			if(ModelState.IsValid)
			{
				if (model.File != null)
					model.PhotoUrl = await AddImageAsync(model.File);
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["Countries"] = new SelectList(_context.Countries.ToList(), "Id", "Name", model.CountryId);
			return View("Index");
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int id) => View(await _context.Cities.FindAsync(id));
		[HttpPost]
		public async Task<ActionResult> Delete(City model)
		{
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		[HttpGet]
		public ActionResult Create(int id)
		{
			ViewBag.Countries = new SelectList(_context.Countries.ToList(), "Id", "Name");
			return View(new City());
		}
		[HttpPost]
		public async Task<ActionResult> Create(City model)
		{
			if(model.CountryId <1)
				ModelState.AddModelError("CountryId", "Данный пункт обязателен к заполнению");
			else if (ModelState.IsValid)
			{
				model.PhotoUrl = await AddImageAsync(model.File);
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.Countries = new SelectList(_context.Countries.ToList(), "Id", "Name", model.CountryId);
			return View("Index");
		}
	}
}
