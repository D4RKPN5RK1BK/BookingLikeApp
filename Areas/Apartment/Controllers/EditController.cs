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

		protected async Task<bool> AllowEditAsync(int? id)
		{
			if (id == null) return false;
			Models.Apartment apartment = await _context.Apartments.FindAsync(id);
			if (apartment == null) return false;
			if (apartment.UserId != _userManager.GetUserId(User)) return false;
			return true;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> BasicInfo(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			BasicInfoViewModel model = new BasicInfoViewModel(await _context.Apartments.FindAsync(id));
			model.SetProps(await _context.Registrations.FindAsync(id));
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BasicInfo(BasicInfoViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments.FindAsync(model.Id);
				apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);
				apartment.Registration.BasicInfo = true;
				apartment.SetBasicInfo(model);
				apartment.Registration.BasicInfo = true;
				_context.Update(apartment.Registration);
				_context.Update(apartment);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index", "Number", new { apartment.Id });
			}
			return View(model);
		}

		public async Task<IActionResult> Rules(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			RulesViewModel model = new RulesViewModel(await _context.Apartments.FindAsync(id));
			model.SetProps(await _context.Registrations.FindAsync(id));

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Rules(RulesViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments.FindAsync(model.Id);
				apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);
				apartment.SetRules(model);
				apartment.Registration.Rules = true;
				_context.Update(apartment);
				_context.Update(apartment.Registration);
				await _context.SaveChangesAsync();
				return RedirectToAction("Payment", "Edit");
			}
			return View(model);
		}

		public async Task<IActionResult> Services(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			ServicesViewModel model = new ServicesViewModel(await _context.Apartments.FindAsync(id));
			model.SetProps(await _context.Registrations.FindAsync(id));
			model.ServicesList = _context.Services.ToList();
			List<ApartmentService> services = _context.ApartmentServices.Where(o => o.ApartmentId == model.Id).ToList();

			for (int i = 0; i < services.Count; i++)
				for (int j = 0; j < model.ServicesList.Count; j++)
					if (services[i].ServiceId == model.ServicesList[j].Id)
						model.ServicesList[j].Select = true;

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Services(ServicesViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments.FindAsync(model.Id);
				apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);
				apartment.SetServices(model);

				_context.RemoveRange(_context.ApartmentServices.Where(o => o.ApartmentId == apartment.Id));
				List<ApartmentService> apartmentServices = new List<ApartmentService>();

				foreach (var item in model.ServicesList.Where(o => o.Select))
				{
					apartmentServices.Add(new ApartmentService() { ApartmentId = apartment.Id, ServiceId = item.Id });
				}
				apartment.Registration.Services = true;

				await _context.ApartmentServices.AddRangeAsync(apartmentServices);
				_context.Update(apartment.Registration);
				_context.Update(apartment);
				await _context.SaveChangesAsync();
				return RedirectToAction("Photos", "Edit");
			}
			return View(model);
		}

		public async Task<IActionResult> Photos(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment apartment = await _context.Apartments.FindAsync(id);
			apartment.Photos = _context.Photos.Where(o => o.ApartmentId == apartment.Id).ToList();
			PhotosViewModel model = new PhotosViewModel(apartment);
			model.SetProps(await _context.Registrations.FindAsync(id));
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddPhoto(PhotosViewModel model)
		{
			Models.Apartment apartment = await _context.Apartments.FindAsync(model.Id);
			apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);


			if (model.File != null)
			{
				int index = _context.Photos.Any() ? _context.Photos.Max(o => o.Id) : 1;

				string path = "/images/" + index.ToString() + model.File.FileName;

				using (var filestream = new FileStream(_env.WebRootPath + path, FileMode.Create))
				{
					await model.File.CopyToAsync(filestream);
				}

				Photo photo = new Photo()
				{
					ApartmentId = apartment.Id,
					PhotoUrl = path,
					Name = index.ToString() + model.File.FileName,
				};

				apartment.Registration.Photos = true;
				_context.Update(apartment.Registration);
				await _context.AddAsync(photo);
				await _context.SaveChangesAsync();

			}
			return RedirectToAction("Photos", "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePhoto(int id)
		{
			Photo photo = await _context.Photos.FindAsync(id);
			if (photo == null) return NotFound();
			Models.Apartment apartment = await _context.Apartments.FindAsync(photo.ApartmentId);
			if (!await AllowEditAsync(apartment.Id)) return NotFound();

			_context.Photos.Remove(photo);
			await _context.SaveChangesAsync();

			return RedirectToAction("Photos", "Edit");
		}

		public async Task<IActionResult> Facilites(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			FacilitesViewModel model = new FacilitesViewModel(await _context.Apartments.FindAsync(id));
			model.SetProps(await _context.Registrations.FindAsync(id));

			return View(model);
		}

		public async Task<IActionResult> Payment(int id)
		{
			if (!await AllowEditAsync(id)) return NotFound();
			Models.Apartment apartment = await _context.Apartments.FindAsync(id);
			apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);

			PaymentViewModel model = new PaymentViewModel(apartment);
			model.SetProps(await _context.Registrations.FindAsync(id));
			User user = await _userManager.GetUserAsync(User);
			model.Polishers = new SelectList(
				new Dictionary<string, bool>()
				{
					{user.UserName, false },
					{apartment.Name == null ? "Данный отель" : apartment.Name , true }
				},
				"Value",
				"Key"
				);
			model.Cards = _context.Cards.ToList();

			for (int i = 0; i < model.Cards.Count; i++)
			{
				if (_context.ApartmentCards.Any(o => o.ApartmentId == apartment.Id && o.CardId == model.Cards[i].Id))
					model.Cards[i].Use = true;
			}
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Payment(PaymentViewModel model)
		{
			if (ModelState.IsValid)
			{
				Models.Apartment apartment = await _context.Apartments.FindAsync(model.Id);
				apartment.Registration = await _context.Registrations.FindAsync(apartment.Id);
				apartment.Registration.Payment = true;
				apartment.SetPayment(model);

				_context.RemoveRange(_context.ApartmentCards.Where(o => o.ApartmentId == apartment.Id));

				List<ApartmentCard> apartmentCards = new List<ApartmentCard>();

				foreach (var item in model.Cards)
				{
					apartmentCards.Add(new ApartmentCard() { ApartmentId = apartment.Id, CardId = item.Id });
				}

				await _context.AddRangeAsync(apartmentCards);
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
					return RedirectToAction(apartment.Registration.Unfinished, "Edit");
				}
			}
			return View(model);
		}

	}
}
