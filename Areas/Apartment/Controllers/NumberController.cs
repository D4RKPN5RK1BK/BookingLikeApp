using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                number.NumberRooms = _context.NumberRooms.Where(o => o.NumberId == number.Id);

            number.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == number.Id);

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

            Number model = await GetNumberAsync(id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Number model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

        public async Task<IActionResult> AdminEdit(int id)
        {
            if (await AllowEditASync(id))
                return NotFound();

            Number model = _context.Numbers.Find(id);

            return View(model);
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult ConfirmDelet()
        {
            return RedirectToAction("Index", "Create");
            return RedirectToAction("Index", "Edit");
        }
    }
}
