using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
	public class ReadController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<User> _userManager;

		public ReadController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			int pageSize = 10;
			User user = await _userManager.GetUserAsync(User);
			UserApartmentsViewModel model = new UserApartmentsViewModel();
			model.Apartments = _context.Apartments.Where(o => o.UserId == user.Id).ToList();

			int count = model.Apartments.Count;

			model.Apartments = model.Apartments.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			for(int i = 0; i < model.Apartments.Count; i++)
			{
				model.Apartments[i].Numbers = _context.Numbers.Where(o => o.ApartmentId == model.Apartments[i].Id).ToList();
			}

			model.PageViewModel = new PageViewModel(count, page, pageSize);
			return View(model);
		}

		[HttpGet]
		public ActionResult Search(string name = "", string city = "", string country = "", int page = 1)
		{
			int pageSize = 10;

			SearchApartmentViewModel model = new SearchApartmentViewModel()
			{
				Name = name,
				City = city,
				Country = country
			};

			if (_context.Apartments.Any(o => o.Name == model.Name))
			{
				model.Apartments = _context.Apartments.Where(o => o.Name == model.Name).ToList();
			}
			else
			{
				model.Apartments = _context.Apartments.Where(o => o.Name.Contains(model.Name)).ToList();
			}

			foreach (var a in model.Apartments)
			{
				a.ApartmentType = _context.ApartmentTypes.Find(a.ApartmentTypeId);
			}

			int count = model.Apartments.Count;
			model.Apartments = model.Apartments.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			for (int i = 0; i < model.Apartments.Count; i++)
				if (_context.Photos.Any(o => o.ApartmentId == model.Apartments[i].Id))
					model.Apartments[i].Photos = _context.Photos.Where(o => o.ApartmentId == model.Apartments[i].Id).ToList();
				else
					model.Apartments[i].Photos = new List<Photo>();

			model.PageModel = new PageViewModel(count, page, pageSize);

			return View(model);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			ApartmentDetailsViewModel model = new ApartmentDetailsViewModel();
			model.Begin = DateTime.Now;
			model.End = DateTime.Now.AddDays(7);
			model.MinDate = DateTime.Now;
			model.MaxDate = DateTime.Now.AddDays(90);
			model.EnableReservation = false;
			model.Apartment = _context.Apartments.Find(id);
			model.Apartment.ApartmentType = _context.ApartmentTypes.Find(model.Apartment.ApartmentTypeId);
			model.Apartment.Numbers = _context.Numbers.Where(o => o.ApartmentId == model.Apartment.Id).ToList();
			model.Apartment.ApartmentServices = _context.ApartmentServices.Where(o => o.ApartmentId == model.Apartment.Id).ToList();
			foreach(var n in model.Apartment.Numbers)
			{
				n.NumberType = _context.NumberTypes.Find(n.NumberTypeId);
				n.NumberBeds = _context.NumberBeds.Where(o => o.NumberId == n.Id).ToList();
				n.NumberEntities = _context.NumberEntities.Where(o => o.NumberId == n.Id).ToList();
				n.NumberServices = _context.NumberServices.Where(o => o.NumberId == n.Id).ToList();
				n.Packs = _context.Packs.Where(o => o.NumberId == n.Id).ToList();
				foreach(var p in n.Packs)
				{
					p.PackServices = _context.PackServices.Where(o => o.PackId == p.Id).ToList();
					p.PackTenants = _context.PackTenants.Where(o => o.PackId == p.Id).ToList();
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult SearchNumber(ApartmentDetailsViewModel model)
		{
			model.MinDate = DateTime.Now;
			model.MaxDate = DateTime.Now.AddDays(90);
			model.Apartment = _context.Apartments.Find(model.Apartment.Id);
			model.EnableReservation = true;
			model.Apartment.ApartmentType = _context.ApartmentTypes.Find(model.Apartment.ApartmentTypeId);
			
			if (model.Name == string.Empty || model.Name == null)
				model.Apartment.Numbers = _context.Numbers.Where(o => o.ApartmentId == model.Apartment.Id).ToList();
			else
				model.Apartment.Numbers = _context.Numbers.Where(o => o.ApartmentId == model.Apartment.Id && o.Name.Contains(model.Name)).ToList();

			model.Apartment.ApartmentServices = _context.ApartmentServices.Where(o => o.ApartmentId == model.Apartment.Id).ToList();
			for(int i = 0; i < model.Apartment.Numbers.Count; i++)
			{
				model.Apartment.Numbers[i].NumberType = _context.NumberTypes.Find(model.Apartment.Numbers[i].NumberTypeId);
				model.Apartment.Numbers[i].NumberBeds = _context.NumberBeds.Where(o => o.NumberId == model.Apartment.Numbers[i].Id).ToList();
				model.Apartment.Numbers[i].NumberEntities = _context.NumberEntities.Where(o => o.NumberId == model.Apartment.Numbers[i].Id).ToList();
				model.Apartment.Numbers[i].NumberServices = _context.NumberServices.Where(o => o.NumberId == model.Apartment.Numbers[i].Id).ToList();
				model.Apartment.Numbers[i].Packs = _context.Packs.Where(o => o.NumberId == model.Apartment.Numbers[i].Id).ToList();

				foreach(var e in model.Apartment.Numbers[i].Packs)
				{
					e.PackServices = _context.PackServices.Where(o => o.PackId == e.Id).ToList();
					e.PackTenants = _context.PackTenants.Where(o => o.PackId == e.Id).ToList();
				}

				for(int j = 0; j < model.Apartment.Numbers[i].Packs.Count; j++)
				{
					model.Apartment.Numbers[i].Packs[j].PackServices = _context.PackServices.Where(o => o.PackId == model.Apartment.Numbers[i].Packs[j].Id).ToList();
					model.Apartment.Numbers[i].Packs[j].PackTenants = _context.PackTenants.Where(o => o.PackId == model.Apartment.Numbers[i].Packs[j].Id).ToList();
				}

				for(int j = 0; j < model.Apartment.Numbers[i].NumberEntities.Count; j++ )
				{
					model.Apartment.Numbers[i].NumberEntities[j].Reservations = new List<Reservation>();
					model.Apartment.Numbers[i].NumberEntities[j].EntityReservations = _context.EntityReservations.Where(o => o.NumberEntityId == model.Apartment.Numbers[i].NumberEntities[j].Id).ToList();
					
					for(int t = 0; t < model.Apartment.Numbers[i].NumberEntities[j].EntityReservations.Count; t++)
					{
						
						if (!model.Apartment.Numbers[i].NumberEntities[j].Reservations.Any(o => o.Id == model.Apartment.Numbers[i].NumberEntities[j].EntityReservations[t].ReservationId))
							model.Apartment.Numbers[i].NumberEntities[j].Reservations.Add(_context.Reservations.Find(model.Apartment.Numbers[i].NumberEntities[j].EntityReservations[t].ReservationId));
					}	
				}

				model.Apartment.Numbers[i].NumberEntities = model.Apartment.Numbers[i].NumberEntities.Where(o => o.Reservations.All(r =>
						   r.ReservationBegin < model.Begin &&
						   r.ReservationEnd < model.Begin ||
						   r.ReservationBegin > model.End &&
						   r.ReservationEnd > model.End &&
						   !r.Cencel		
				)).ToList();
			}

			model.Apartment.Numbers = model.Apartment.Numbers.Where(o => o.NumberEntities.Count > 0).ToList();

			return View("Details", model);
		}

		[HttpGet]
		public ActionResult Price(int id)
		{
			if (_context.PackTenants.Any(o => o.Id == id))
			{
				return Json(_context.PackTenants.Find(id).Price);
			}
			return Json(0);
		}
	}
}
