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
			EditNumberViewModel model = new EditNumberViewModel();
			model.Number = await _context.Numbers.FindAsync(id);
			
			if (!await AllowEditASync(model.Number))
				return NotFound();

			model.Count = _context.NumberEntities.Count(o => o.NumberId == model.Number.Id);
			model.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");
			model.Rooms = _context.Rooms.ToList();

			if (_context.Registrations.Any(o => o.ApartmentId == model.Number.ApartmentId))
				model.SetProps(_context.Registrations.Find(model.Number.ApartmentId));

			model.Number.NumberType = await _context.NumberTypes.FindAsync(model.Number.NumberTypeId);
			model.Number.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();
			if (model.Number.NumberType.HasRooms)
				model.Number.NumberRooms = _context.NumberRooms.Where(o => o.NumberId == model.Number.Id).ToList();

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditNumberViewModel model)
		{
			if (!await AllowEditASync(model.Number))
				return NotFound();
			if (ModelState.IsValid)
			{
				model.Number.Finish = true;
				_context.Update(model.Number);

				if (_context.Registrations.Any( o => o.ApartmentId == model.Number.ApartmentId))
				{
					Registration registration = await _context.Registrations.FindAsync(model.Number.ApartmentId);
					registration.Numbers = true;
					_context.Update(registration);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { model.Number.Id });
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			if (!await AllowEditASync(await _context.Numbers.FindAsync(id)))
				return NotFound();
			return RedirectToAction("Index", new { id });
		}
	}
}
