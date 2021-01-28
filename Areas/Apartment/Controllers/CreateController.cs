using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
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
    public class CreateController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CreateController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        protected async Task<Models.Apartment> GetUnfinished()
        {
            User user = await _userManager.GetUserAsync(User);
            Models.Apartment apartment;

            if (_context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id))
            {
                apartment = _context.Apartments.FirstOrDefault(o => o.Finished == false && o.UserId == user.Id);
            }
            else
            {
                apartment = new Models.Apartment() { UserId = user.Id, ApartmentTypeId = 1, StreetId = 1};
                _context.Apartments.Add(apartment);
                await _context.SaveChangesAsync();
            }
            return apartment;
        }

        public IActionResult Index()
        {
            ViewBag.ApartmentTypes = _context.ApartmentTypes.ToList();
            return View();
        }

        public IActionResult Preset(int id)
        {
            return View();
        }

        public async Task<IActionResult> BasicInfo()
        { 
            Models.Apartment apartment = await GetUnfinished();
            BasicInfoViewModel model = new BasicInfoViewModel()
            {
                Name = apartment.Name,
                Stars = apartment.Stars,
                StreetId = apartment.StreetId,
                SecondAddressLine = apartment.SecondAddressLine,
                ContactPerson = apartment.ContactPerson,
                ContactNumber = apartment.ContactNumber,
                AdditionalNumber = apartment.AdditionalNumber,
                Description = apartment.Description,
            };
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"Без звезд", 0},
                {"Одна звезда", 1},
                {"Две звезды", 2},
                {"Три звезды", 3},
                {"Четыре звезды", 4},
                {"Пять звезд", 5},
            };
            SelectList stars = new SelectList(dict, "Value", "Key");
            ViewBag.Stars = stars;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BasicInfo(BasicInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Apartment apartment = await GetUnfinished();
                apartment.Name = model.Name;
                apartment.Stars = model.Stars;
                apartment.StreetId = model.StreetId;
                apartment.SecondAddressLine = model.SecondAddressLine;
                apartment.ContactPerson = model.ContactPerson;
                apartment.ContactNumber = model.ContactNumber;
                apartment.AdditionalNumber = model.AdditionalNumber;
                apartment.Description = model.Description;
                _context.Update(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Numbers", "Create");
            }
            return View(model);
        }

        public IActionResult Numbers()
        {
            return View(); 
        }

        public async Task<IActionResult> Rules()
        {
            Models.Apartment model = await GetUnfinished();
            return View(model);
        }

        public async Task<IActionResult> Services()
        {
            Models.Apartment model = await GetUnfinished();
            return View(model);
        }

        public IActionResult Photos()
        {
            return View();
        }

        public async Task<IActionResult> Facilites()
        {
            Models.Apartment model = await GetUnfinished();
            return View(model);
        }

        public async Task<IActionResult> Payment()
        {
            Models.Apartment model = await GetUnfinished();
            return View(model);
        }
    }
}
