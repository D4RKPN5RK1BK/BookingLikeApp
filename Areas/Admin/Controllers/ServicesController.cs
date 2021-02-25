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
	public class ServicesController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly IWebHostEnvironment _env;
		
		public ServicesController(ApplicationDbContext context, IWebHostEnvironment env)
		{
			_context = context;
			_env = env;
		}

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

		public IActionResult Index() => View(_context.Services.ToList());

		public IActionResult Create() => View(new Service());
		[HttpPost]
		public async Task<IActionResult> Create(Service model)
		{
			if(ModelState.IsValid)
			{
				model.IconUrl = await AddImageAsync(model.File);
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Edit(int id) => View(await _context.Services.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(Service model)
		{
			if(ModelState.IsValid)
			{
				model.IconUrl = await AddImageAsync(model.File);
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.Services.FindAsync(id));
		[HttpPost]
		public IActionResult Delete(Service model)
		{
			_context.Remove(model);
			return RedirectToAction("Index");
		}
	}
}
