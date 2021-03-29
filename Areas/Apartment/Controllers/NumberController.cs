using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("apartment")]
	public class NumberController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;

		public NumberController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		protected async Task<bool> AllowEditASync(Number number)
		{

			User user = await _userManager.GetUserAsync(User);
			if (number == null) return false;

			Models.Apartment apartment = _context.Apartments.Find(number.ApartmentId);
			if (apartment.UserId != user.Id) return false;

			return true;
		}

		public ActionResult GetBeds()
		{
			return Json(_context.Beds);
		}

		public async Task<IActionResult> Index(int? id)
		{
			User user = await _userManager.GetUserAsync(User);
			Models.Apartment apartment = _context.Apartments.Find(id);
			if (apartment == null) return NotFound();
			if (apartment.UserId != user.Id) return NotFound();

			NumbersViewModel model = new NumbersViewModel(apartment);

			if (!apartment.Finished) model.SetProps(_context.Registrations.Find(id));
			model.NumbersList = _context.Numbers.Where(o => o.ApartmentId == id).ToList();
			model.NumberTypes = new SelectList(_context.NumberTypes.ToList(), "Id", "Name");

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(int id, int numberTypeId)
		{
			User user = await _userManager.GetUserAsync(User);
			Models.Apartment apartment = await _context.Apartments.FindAsync(id);
			if (apartment == null) return NotFound();
			if (apartment.UserId != user.Id) return NotFound();
			NumberType numberType = await _context.NumberTypes.FindAsync(numberTypeId);
			if (numberType == null) return NotFound();
		
			await _context.Numbers.AddAsync(new Number() { NumberTypeId = numberTypeId, ApartmentId = id, Name = numberType.Name});
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Number", new { id });
			
		}

		public async Task<IActionResult> Edit(int? id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			
			if (!await AllowEditASync(model))
				return NotFound();

			model.Count = _context.NumberEntities.Count(o => o.NumberId == model.Id);
			ViewBag.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");
			ViewBag.Rooms = _context.Rooms.ToList();


			model.NumberType = await _context.NumberTypes.FindAsync(model.NumberTypeId);
			model.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();
			if (model.NumberType.HasRooms)
				model.NumberRooms = _context.NumberRooms.Where(o => o.NumberId == model.Id).ToList();

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Number model)
		{
			if (!await AllowEditASync(model))
				return NotFound();
			if (ModelState.IsValid)
			{
				model.Finish = true;
				_context.Update(model);

				if (_context.Registrations.Any( o => o.ApartmentId == model.ApartmentId))
				{
					Registration registration = await _context.Registrations.FindAsync(model.ApartmentId);
					registration.Numbers = true;
					_context.Update(registration);
				}

				if (model.Count > 0)
				{
					model.NumberEntities = new List<NumberEntity>();
					for(int i = 0; i < model.Count; i++)
					{
						
						model.NumberEntities.Add(new NumberEntity { NumberId = model.Id });

					}
					await _context.NumberEntities.AddRangeAsync(model.NumberEntities);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { model.Id });
			}
			return View(model);
		}

		public async Task<ActionResult> Delete(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Number model)
		{
			int id = model.ApartmentId;
			_context.Remove(model);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Number",  new { id });
		}

		public async Task<ActionResult> Reservations(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			return View(model);
		}

		public async Task<ActionResult> Payment(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			model.Packs = _context.Packs.Where(o => o.NumberId == model.Id).ToList();

			foreach(var i in model.Packs)
			{
				i.PackServices = _context.PackServices.Where(o => o.PackId == i.Id).ToList();
				i.PackTenants = _context.PackTenants.Where(o => o.PackId == i.Id).ToList();
			}

			ViewBag.NumberServices = _context.NumberServices.Where(o => o.NumberId == model.Id).ToList();

			return View(model);
		}

		public async Task<ActionResult> Entities(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);

			model.Count = _context.NumberEntities.Count(o => o.NumberId == model.Id);
			model.NumberEntities = _context.NumberEntities.Where(o => o.NumberId == model.Id).ToList();

			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			return View(model);
		}

		public async Task<ActionResult> Beds(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);

			ViewBag.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");
			ViewBag.Rooms = _context.Rooms.ToList();

			model.NumberType = await _context.NumberTypes.FindAsync(model.NumberTypeId);
			model.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();
			if (model.NumberType.HasRooms)
				model.NumberRooms = _context.NumberRooms.Where(o => o.NumberId == model.Id).ToList();

			return View(model);
		}

		public async Task<ActionResult> Services(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			model.NumberServices = _context.NumberServices.Where(o => o.NumberId == model.Id).ToList();
			return View(model);
		}

		public async Task<ActionResult> Table(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			model.NumberEntities = _context.NumberEntities.Where(o => o.NumberId == model.Id).ToList();
			for(int i = 0; i < model.NumberEntities.Count; i++)
			{
				model.NumberEntities[i].EntityReservations = _context.EntityReservations.Where(o => o.NumberEntityId == model.NumberEntities[i].Id).ToList();
			}
			List<DateTime> dates = new List<DateTime>();
			TimeSpan twoWeeks = TimeSpan.FromDays(14);
			DateTime start = DateTime.Now;
			start = start.Subtract(twoWeeks);
			dates.Add(start);
			for(int i = 0; i < 28; i++)
			{
				dates.Add(dates[i]);
				dates[i + 1] = dates[i + 1].AddDays(1);
			}
			ViewBag.Dates = dates;
			return View(model);
		}
	}
}
