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

        protected async Task<bool> AllowEditASync(int? id)
        {
            if (id == null)
                return false;

            User user = await _userManager.GetUserAsync(User);
            List<Models.Apartment> apartments = _context.Apartments.Where(o => o.UserId == user.Id).ToList();

            foreach (var item in apartments)
                if (_context.Numbers.Any(o => o.Id == id))
                    return true;

            return false;
        }

        protected async Task<Number> GetNumberAsync(int? id)
        {
            Number number = await _context.Numbers.FindAsync(id);
            number.NumberType = await _context.NumberTypes.FindAsync(number.NumberTypeId);

            if (number.NumberType.HasRooms)
                number.NumberRooms = _context.NumberRooms.Where(o => o.NumberId == number.Id).ToList();

            number.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == number.Id).ToList();

            return number;
        }

        public IActionResult Index(int id)
        {            
            
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!await AllowEditASync(id))
                return NotFound();

			Number number = await GetNumberAsync(id);
			EditNumberViewModel model = new EditNumberViewModel(number);
            if (_context.Registrations.Any(o => o.ApartmentId == model.ApartmentId))
                model.SetProps(_context.Registrations.FirstOrDefault(o => o.ApartmentId == model.ApartmentId));
            model.BedsCount = _context.NumberBeds.Where(o => o.NumberId == id).Count();
            model.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == id).ToList();
            model.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");
            model.RoomsSelect = new SelectList(_context.Rooms.ToList(), "Id", "Name");
            model.SetProps(_context.Registrations.Find(number.ApartmentId));

            return View(model);
        }

        /*[Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminEdit(int id)
        {
            if (!_context.Numbers.Any(o => o.Id == id))
                return NotFound();

            EditNumberViewModel model = new EditNumberViewModel(await GetNumberAsync(id));
            model.BedsSelect = new SelectList(_context.Beds.ToList(), "Id", "Name");
            model.RoomsSelect = new SelectList(_context.Rooms.ToList(), "Id", "Name");
            model.SetProps(_context.Registrations.Find(model.ApartmentId));
            return View(model);
        }*/

        [HttpPost("{id}")]
        public async Task<IActionResult> Basic(EditNumberViewModel model, int id)
        {
			if (ModelState.IsValid)
			{
				Number number = await _context.Numbers.FindAsync(id);
				number.SetProps(model);
				_context.Numbers.Update(number);

				/*Registration registration = await _context.Registrations.FindAsync(number.ApartmentId);
				registration.Numbers = true;
				_context.Registrations.Update(registration);*/

				await _context.SaveChangesAsync();

			}
            return RedirectToAction("Edit", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> BedsCount(EditNumberViewModel model, int? id)
        {
            if (ModelState["BedsCount"].Errors.Count == 0)
            {
                List<NumberBed> numberBeds = new List<NumberBed>();
                int bedsCount = _context.NumberBeds.Where(o => o.NumberId == id).Count();
                if (bedsCount < model.BedsCount)
                {
                    for (int i = 0; i < model.BedsCount - bedsCount; i++)
                        numberBeds.Add(new NumberBed() { NumberId = (int)id, BedId = _context.Beds.First().Id });
                    await _context.NumberBeds.AddRangeAsync(numberBeds);

                }
                else if (bedsCount > model.BedsCount)
                {
                    numberBeds = _context.NumberBeds.Where(o => o.NumberId == (int)id).Take(bedsCount - model.BedsCount).ToList();
                    _context.RemoveRange(numberBeds);
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> RoomsCount(EditNumberViewModel model, int? id)
        {
            return View(model);
        }

		/*[HttpPost]
        public async Task<IActionResult> Beds(EditNumberViewModel model, int? id)
        {
            if (model.NumberBeds != null & model.NumberBeds.Count > 0)
                for (int i = 0; i < model.NumberBeds.Count; i++)
                    model.NumberBeds[i].NumberId = (int)id;

            _context.NumberBeds.UpdateRange(model.NumberBeds);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id });
        }*/

		[HttpPost]
		public async Task<IActionResult> Beds(EditNumberViewModel model, int? id)
		{
			if (model.NumberBeds != null && model.NumberBeds.Count > 0)
			{
				for (int i = 0; i < model.NumberBeds.Count; i++)
					model.NumberBeds[i].NumberId = (int)id;
				_context.NumberBeds.UpdateRange(model.NumberBeds);
			}
			await _context.SaveChangesAsync();
			return RedirectToAction("Edit", new { id });
		}

		[HttpPost]
        public async Task<IActionResult> Rooms(EditNumberViewModel model)
        {

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult ConfirmDelete()
        {
            return RedirectToAction("Index", "Create");
            return RedirectToAction("Index", "Edit");
        }
    }
}
