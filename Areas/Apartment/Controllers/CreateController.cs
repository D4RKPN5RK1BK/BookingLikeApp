using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
    [Area("apartment")]
    public class CreateController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateController(UserManager<User> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }

        protected async Task<Models.Apartment> GetUnfinishedAsync()
        {
            User user = await _userManager.GetUserAsync(User);
            Models.Apartment apartment = _context.Apartments.FirstOrDefault(o => o.Finished == false && o.UserId == user.Id);
            apartment.Registration = _context.Registrations.Find(apartment.Id);
            ViewBag.IsFinished = apartment.Registration.FinishedDictionary;
            return apartment;
        }

        protected async Task<SelectList> GetTimePointsAsync()
        {
            DateTime time = new DateTime();
            Dictionary<string, DateTime> timePoints = new Dictionary<string, DateTime>()
            {
                {"7:00", time.AddHours(7)},
                {"7:30", time.AddHours(7).AddMinutes(30)},
                {"8:00", time.AddHours(8)},
                {"8:30", time.AddHours(8).AddMinutes(30)},
                {"9:00", time.AddHours(9)},
                {"9:30", time.AddHours(9).AddMinutes(30)},
                {"10:00", time.AddHours(10)},
                {"10:30", time.AddHours(10).AddMinutes(30)},
                {"11:00", time.AddHours(11)},
                {"11:30", time.AddHours(11).AddMinutes(30)},
                {"12:00", time.AddHours(12)},
                {"12:30", time.AddHours(12).AddMinutes(30)},
                {"13:00", time.AddHours(13)},
                {"13:30", time.AddHours(13).AddMinutes(30)},
                {"14:00", time.AddHours(14)},
                {"14:30", time.AddHours(14).AddMinutes(30)},
                {"15:00", time.AddHours(15)},
                {"15:30", time.AddHours(15).AddMinutes(30)},
                {"16:00", time.AddHours(16)},
                {"16:30", time.AddHours(16).AddMinutes(30)},
                {"17:00", time.AddHours(17)},
                {"17:30", time.AddHours(17).AddMinutes(30)},
                {"18:00", time.AddHours(18)},
                {"18:30", time.AddHours(18).AddMinutes(30)},
                {"19:00", time.AddHours(19)},
                {"19:30", time.AddHours(19).AddMinutes(30)},
                {"20:00", time.AddHours(20)},
            };
            return new SelectList(timePoints, "Value", "Key");
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            ViewData["Exist"] = _context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id);
            ViewBag.ApartmentTypes = _context.ApartmentTypes.ToList();
            return View();
        }

        public async Task<IActionResult> Initialize(int apartmentType) 
        {
            User user = await _userManager.GetUserAsync(User);
            Models.Apartment apartment;
            apartment = new Models.Apartment() { UserId = user.Id, ApartmentTypeId = 1, ApartmentStreetId = 1 };
            Registration registration = new Registration() { Apartment = apartment };
            await _context.AddAsync(registration);
            await _context.AddAsync(apartment);
            await _context.SaveChangesAsync();

            ViewBag.IsFinished = apartment.Registration.FinishedDictionary;

            return RedirectToAction("BasicInfo", "Create");
        }

        public async Task<IActionResult> BasicInfo()
        {
            BasicInfoViewModel model = new BasicInfoViewModel(await GetUnfinishedAsync());
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BasicInfo(BasicInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Apartment apartment =  await GetUnfinishedAsync();
                apartment.Registration.BasicInfo = true;
                apartment.SetBasicInfo(model);
                apartment.Registration.BasicInfo = true;
                _context.Update(apartment.Registration);
                _context.Update(apartment);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Numbers", "Create");
            }
            return View(model);
        }

        public async Task<IActionResult> Numbers()
        {
            Models.Apartment apartment = await GetUnfinishedAsync();
            return View(); 
        }

        public async Task<IActionResult> Rules()
        {
            ViewBag.TimePoints = await GetTimePointsAsync();
            RulesViewModel model = new RulesViewModel(await GetUnfinishedAsync());
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rules(RulesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Apartment apartment = await GetUnfinishedAsync();
                apartment.SetRules(model);
                apartment.Registration.Rules = true;
                _context.Update(apartment);
                _context.Update(apartment.Registration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Payment", "Create");
            ViewBag.TimePoints = await GetTimePointsAsync();
            }
            return View(model);
        }

        public async Task<IActionResult> Services()
        {
            ServicesViewModel model = new ServicesViewModel(await GetUnfinishedAsync());
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Services(ServicesViewModel model)
        {
            if(ModelState.IsValid)
            {
                Models.Apartment apartment = await GetUnfinishedAsync();
                apartment.SetServices(model);
                apartment.Registration.Services = true;
                _context.Update(apartment.Registration);
                _context.Update(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Photos", "Create");
            }
            return View(model);
        }

        public async Task<IActionResult> Photos()
        {
            Models.Apartment apartment = await GetUnfinishedAsync();
            return View(_context.Photos.Where(o => o.ApartmentId == apartment.Id).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            Models.Apartment apartment = await GetUnfinishedAsync();

            if (file != null)
            {
                int index = _context.Photos.Any() ? _context.Photos.Max(o => o.Id) : 1;

                string path = "/images/" + index.ToString() + file.FileName;

                using (var filestream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                Photo photo = new Photo()
                {
                    ApartmentId = apartment.Id,
                    PhotoUrl = path,
                    Name = index.ToString() + file.FileName,
                };

                apartment.Registration.Photos = true;
                _context.Update(apartment.Registration);
                await _context.AddAsync(photo);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("Photos", "Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhoto(int photo)
        {
            Models.Apartment apartment = await GetUnfinishedAsync();
            List<Photo> photos = _context.Photos.Where(o => o.ApartmentId == apartment.Id).ToList();
            
            if (photos.Any(o => o.Id == photo))
            {
                _context.Photos.Remove(_context.Photos.Find(photo));
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Photos", "Create");
        }

        public async Task<IActionResult> Facilites()
        {
            Models.Apartment model = await GetUnfinishedAsync();
            return View(model);
        }

        public async Task<IActionResult> Payment()
        {
            PaymentViewModel model = new PaymentViewModel();
            return View(model);
        }
        public async Task<IActionResult> Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}
