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

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            ViewData["Exist"] = _context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id);
            ViewBag.ApartmentTypes = _context.ApartmentTypes.ToList();
            return View();
        }

        public async Task<IActionResult> Initialize(int apartmentType) 
        {
            //Придумать как заменить дефолтные значения с тестовых типов на настоящие
            User user = await _userManager.GetUserAsync(User);
            Models.Apartment apartment;
            if (_context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id))
            {
                apartment = _context.Apartments.FirstOrDefault(o => o.Finished == false && o.UserId == user.Id);
                _context.Remove(apartment);
            }
            
            apartment = new Models.Apartment() { UserId = user.Id, ApartmentTypeId = 1, ApartmentStreetId = 1 };
            Registration registration = new Registration() { Apartment = apartment };
            Number number = new Number() { ApartmentId = apartment.Id, NumberTypeId = 1};
            await _context.AddAsync(registration);
            await _context.AddAsync(apartment);
            await _context.SaveChangesAsync();

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
            List<Models.Number> numbers = new List<Number>()
            {
                _context.Numbers.FirstOrDefault(o => o.ApartmentId == apartment.Id)
            };
            apartment.Numbers = numbers;
            NumbersViewModel model = new NumbersViewModel(apartment);
            return View(model); 
        }

        public async Task<IActionResult> Rules()
        {
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
            apartment.Photos = _context.Photos.Where(o => o.ApartmentId == apartment.Id).ToList();
            PhotosViewModel model = new PhotosViewModel(apartment);
            return View(model);
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
            FacilitesViewModel model = new FacilitesViewModel(await GetUnfinishedAsync());
            return View(model);
        }
        
        public async Task<IActionResult> Payment()
        {
            PaymentViewModel model = new PaymentViewModel(await GetUnfinishedAsync());

            model.Cards = _context.Cards.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Apartment apartment = await GetUnfinishedAsync();
                apartment.Registration.Payment = true;
                apartment.SetPayment(model);
                _context.Update(apartment);
                _context.Update(apartment.Registration);
                await _context.SaveChangesAsync();
                if (apartment.Registration.IsFinished)
                {
                    apartment.Finished = true;
                    apartment.Enable = model.Enable;
                    _context.Update(apartment);
                    _context.Remove(apartment.Registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction(apartment.Registration.Unfinished, "Create");
                }
            }
            return View(model);
        }
    }
}
