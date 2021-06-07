using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("Apartment")]
	public class EditController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _env;

		public EditController(UserManager<User> userManager, ApplicationDbContext context, IWebHostEnvironment env)
		{
			_userManager = userManager;
			_context = context;
			_env = env;
		}

		private static SelectList GetTimeList()
		{
			Dictionary<string, DateTime> timeDictionary = new Dictionary<string, DateTime>();
			for (int i = 1; i <= 24; i++)
				timeDictionary.Add(DateTime.MinValue.AddHours(i).ToString("HH:mm"), DateTime.MinValue.AddHours(i));

			return new SelectList(timeDictionary, "Value", "Key");
		}

		protected async Task<bool> AllowEditAsync(int? id)
		{
			if (id == null) return false;
			Models.Apartment apartment = await _context.Apartments.FindAsync(id);
			if (apartment == null) return false;
			if (apartment.UserId != _userManager.GetUserId(User)) return false;
			return true;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment model = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == id);
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> BasicInfo(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();

			Models.Apartment model = _context.Apartments
				.Include(o => o.Registration)
				.Include(o => o.City)
				.Include(o => o.Country)
				.First(o => o.Id == id);

			var viewModel = new BasicInfoViewModel()
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Stars = model.Stars,
				ContactPerson = model.ContactPerson,
				ContactPhone = model.ContactPhone,
				AdditionalPhone = model.AdditionalPhone,
				CityId = model.CityId,
				CountryId = model.CountryId,
				SecondAddressLine = model.SecondAddressLine,
				Registration = model.Registration,
				Finished = model.Finished
			};

			ViewData["Countries"] = new SelectList(_context.Countries.ToArray(), "Id", "Name", model.CountryId);
			ViewData["Cities"] = new SelectList(_context.Cities.Where(o => o.CountryId == model.CountryId).ToArray(), "Id", "Name", model.CityId);
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> BasicInfo(BasicInfoViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == model.Id);

				apartment.Name = model.Name;
				apartment.Description = model.Description;
				apartment.Stars = model.Stars;
				apartment.ContactPerson = model.ContactPerson;
				apartment.ContactPhone = model.ContactPhone;
				apartment.AdditionalPhone = model.AdditionalPhone;
				apartment.CityId = model.CityId;
				apartment.CountryId = model.CountryId;
				apartment.SecondAddressLine = model.SecondAddressLine;

				if(apartment.Registration != null)
					apartment.Registration.BasicInfo = true;

				_context.Update(apartment);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Number", new { apartment.Id });
			}

			model.Registration = _context.Registrations.Find(model.Id);
			ViewData["Countries"] = new SelectList(_context.Countries.ToArray(), "Id", "Name", model.CountryId);
			ViewData["Cities"] = new SelectList(_context.Cities.Where(o => o.CountryId == model.CountryId).ToArray(), "Id", "Name", model.CityId);
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Rules(int id)
		{
			Models.Apartment model = _context.Apartments
				.Include(o => o.Registration)
				.First(o => o.Id == id);
			if (model.UserId != _userManager.GetUserId(User)) return NotFound();

			var viewModel = new RulesViewModel()
			{
				Id = model.Id,
				Name = model.Name,
				ArrivalTimeStarts = model.ArrivalTimeStarts,
				ArrivalTimeEnds = model.ArrivalTimeEnds,
				DepartureTimeStarts = model.DepartureTimeStarts,
				DepartureTimeEnds = model.DepartureTimeEnds,
				/*DaysUntilCancelEnds = model.DaysUntilCancelEnds,
				CancelPrice = model.CancelPrice,*/
				Registration = model.Registration,
				Finished = model.Finished
			};

			ViewBag.TimeList = GetTimeList();
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Rules(RulesViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == model.Id);
				if (apartment.UserId != _userManager.GetUserId(User)) return NotFound();

				apartment.ArrivalTimeStarts = model.ArrivalTimeStarts;
				apartment.ArrivalTimeEnds = model.ArrivalTimeEnds;
				apartment.DepartureTimeStarts = model.DepartureTimeStarts;
				apartment.DepartureTimeEnds = model.DepartureTimeEnds;
				/*apartment.DaysUntilCancelEnds = model.DaysUntilCancelEnds;
				apartment.CancelPrice = model.CancelPrice;*/

				if (apartment.Registration != null)
					apartment.Registration.Rules = true;

				_context.Update(apartment);
				await _context.SaveChangesAsync();


				if(apartment.Registration?.IsFinished ?? false || apartment.Finished) 
					return RedirectToAction("Enable", "Edit", new { apartment.Id });
				else	
					return RedirectToAction("BasicInfo", "Edit", new { apartment.Id });
			}

			model.Registration = _context.Registrations.Find(model.Id);
			ViewBag.TimeList = GetTimeList();
			return View(model);
		}

		//Сервисы отеля

		[HttpGet]
		public async Task<ActionResult> Services(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment model = await _context.Apartments
					.Include(o => o.Registration)
					.Include(o => o.ApartmentServices)
					.FirstAsync(o => o.Id == id);

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> CreateService([FromBody]int id)
		{
			ApartmentService model = new ApartmentService()
			{
				Name = "Новый сервис",
				HavePrice = false,
				Price = null,
				ApartmentId = id
			};
			await _context.AddAsync(model);

			if(_context.Registrations.Any(o => o.ApartmentId == id))
			{
				Registration registration = _context.Registrations.Find(id);
				registration.Services = true;
				_context.Update(registration);
			}

			await _context.SaveChangesAsync();
			return Json(model);
		}

		[HttpDelete]
		public void DeleteService([FromBody]int id)
		{
			_context.Remove(_context.ApartmentServices.Find(id));
			_context.SaveChanges();
		}

		[HttpPut]
		public async Task UpdateService([FromBody]ApartmentService model)
		{
			if (model.Price == null)
				model.HavePrice = false;
			
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		//Фотографии отеля

		public async Task<ActionResult> Photos(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment model = await _context.Apartments
					.Include(o => o.Registration)
					.Include(o => o.Photos)
					.FirstAsync(o => o.Id == id);
			
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddPhoto(IFormFile file, int id)
		{
			Models.Apartment apartment = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == id);
			if (apartment.UserId != _userManager.GetUserId(User)) return NotFound();

			if (file != null)
			{
				var guid = Guid.NewGuid();
				string path = "/images/" + guid + file.FileName;

				using (var filestream = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(filestream);
				}

				Photo photo = new Photo()
				{
					ApartmentId = id,
					PhotoUrl = path,
					Name = guid + file.FileName,
				};

				if (apartment.Registration != null)
				{
					apartment.Registration.Photos = true;
					_context.Update(apartment.Registration);
				}

				await _context.AddAsync(photo);
				await _context.SaveChangesAsync();

			}
			return RedirectToAction("Photos", "Edit", new { apartment.Id });
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeletePhoto(int id)
		{
			Photo photo =  _context.Photos.Find(id);

			Models.Apartment apartment = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == photo.ApartmentId);

			if (apartment.UserId != _userManager.GetUserId(User)) return NotFound();

			_context.Photos.Remove(photo);
			await _context.SaveChangesAsync();

			return RedirectToAction("Photos", "Edit", new { apartment.Id });
		}

		[HttpGet]
		public async Task<ActionResult> Enable(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment model = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == id);

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Enable(int id, bool enable)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment model = await _context.Apartments
					.Include(o => o.Registration)
					.FirstAsync(o => o.Id == id);

			if(model.Registration != null && model.Registration.IsFinished)
			{
				model.Finished = true;
				_context.Remove(model.Registration);
				_context.Update(model).Property(o => o.Finished);
			}
				
			model.Enable = enable;
			_context.Update(model).Property(o => o.Enable);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Read");
		}
	}
}

