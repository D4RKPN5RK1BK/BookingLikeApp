using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

		public ActionResult GetBeds() => Json(_context.Beds);

		public async Task<ActionResult> Index(int id)
		{
			if (!await _context.Apartments.AnyAsync(o => o.Id == id && o.UserId == _userManager.GetUserId(User)))
				return NotFound();

			Models.Apartment model = await _context.Apartments
				.Include(o => o.Registration)
				.FirstAsync(o => o.Id == id);

			model.Numbers = _context.Numbers
				.Include(o => o.NumberType)
				.Where(o => o.ApartmentId == model.Id).ToList();

			ViewBag.NumberTypeItems = new SelectList(_context.NumberTypes.ToList(), "Id", "Name");
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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

		public async Task<IActionResult> Edit(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);
			
			if (!await AllowEditASync(model))
				return NotFound();

			model.Count = _context.NumberEntities.Count(o => o.NumberId == model.Id);
			ViewBag.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");

			model.NumberType = await _context.NumberTypes.FindAsync(model.NumberTypeId);
			model.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();

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
				await _context.SaveChangesAsync();

				return RedirectToAction("Beds", new {id = model.Id });
			}
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
			Number model = await _context.Numbers
				.Include(o => o.Apartment)
				.Include(o => o.NumberEntities)
				.ThenInclude(o => o.EntityReservations)
				.FirstAsync(o => o.Id == id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Number model)
		{
			model.NumberEntities = _context.NumberEntities
				.Include(o => o.EntityReservations)
				.Where(o => o.NumberId == model.Id).ToList();
			
			int id = model.ApartmentId;
			
			if (!(model.NumberEntities.Sum(o => o.EntityReservations.Count) > 0)) {
				_context.Remove(model);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index",  new { id });
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
			Number model = await _context.Numbers
				.Include(o => o.Apartment)
				.Include(o => o.NumberEntities)
				.ThenInclude(o => o.EntityReservations)
				.FirstAsync(o => o.Id == id);
			model.Count = model.NumberEntities.Sum(o => o.EntityReservations.Count);
			return View(model);
		}

		public async Task<ActionResult> Beds(int id)
		{
			Number model = await _context.Numbers.FindAsync(id);
			model.Apartment = await _context.Apartments.FindAsync(model.ApartmentId);

			ViewBag.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");

			model.NumberType = await _context.NumberTypes.FindAsync(model.NumberTypeId);
			model.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();

			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Services(int id)
		{
			var model = await _context.Numbers
				.Include(o => o.Apartment)
				.Include(o => o.NumberServices)
				.FirstAsync(o => o.Id == id);
			
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Reservations(int id, DateTime? begin, DateTime? end)
		{
			var number = await _context.Numbers
				.Include(o => o.Apartment)
				.FirstAsync(o => o.Id == id);
			number.NumberEntities = _context.NumberEntities.Where(o => o.NumberId == number.Id)
				.Include(o => o.EntityReservations)
				.ToList();

			if(begin == null || end == null)
			{
				begin = DateTime.Today.Subtract(new TimeSpan(14, 0, 0, 0));
				end = DateTime.Today.AddDays(14);
			}
			
			List<DateTime> dates = new List<DateTime>();
			for (DateTime i = (DateTime)begin; i < end; i = i.AddDays(1))
				dates.Add(i);

			var model = new NumberReservationsViewModel()
			{
				Id = number.Id,
				Name = number.Name,
				Apartment = number.Apartment,
				ApartmentId = number.ApartmentId,
				NumberEntities = number.NumberEntities,
				Reservations = _context.Reservations
					.Include(o => o.EntityReservations)
					.Include(o => o.User)
					.Where(o => o.ApartmentId == number.ApartmentId).ToList(),
				Dates = dates,
				/*FilterModel = new NumberReservationsFilter()
				{
					Begin = begin ?? DateTime.Today.Subtract(new TimeSpan(14, 0, 0, 0)),
					End = end ?? DateTime.Today.AddDays(14)
				}*/
			};
			model.FilterModel = new NumberReservationsFilter()
			{
				Begin = begin ?? DateTime.Today.Subtract(new TimeSpan(14, 0, 0, 0)),
				End = end ?? DateTime.Today.AddDays(14)
			};

			return View(model);
		}
	}
}
