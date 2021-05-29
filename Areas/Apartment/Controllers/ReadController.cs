using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Enums;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

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

		protected Models.Apartment GetApartment(int id)
		{
			Models.Apartment apartment = _context.Apartments
				.Include(o => o.Photos)
				.Include(o => o.City)
				.Include(o => o.Country)
				.Include(o => o.ApartmentServices)
				.Include(o => o.ApartmentType)
				.Include(o => o.Reviews)
					.ThenInclude(o => o.ReviewScores)
				.First(o => o.Id == id);

			apartment.Scores = _context.Scores.ToList();

			apartment.Numbers = _context.Numbers.Where(o => o.ApartmentId == apartment.Id && o.Enable && o.Finish)
				.Include(o => o.NumberEntities)
				.Include(o => o.NumberType)
				.Include(o => o.NumberServices)
				.Include(o => o.NumberBeds)
					.ThenInclude(o => o.Bed)
				.ToList();

			for(int i = 0; i< apartment.Scores.Count; i++)
				apartment.Scores[i].AvgValue = (float)apartment.Reviews?
					.Where(o => o.ReviewScores.Count > 0)
					.DefaultIfEmpty()
					.Average(o => o.ReviewScores?
						.Where(r => r.ScoreId == apartment.Scores[i].Id)
						.DefaultIfEmpty()
						.Average(r => r?.Value));

			for (int i = 0; i < apartment.Numbers.Count; i++)
			{
				apartment.Numbers[i].Packs = _context.Packs.Where(o => o.NumberId == apartment.Numbers[i].Id && o.Enable)
					.Include(o => o.PackServices)
					.Include(o => o.PackTenants)
					.ToList();

				for (int j = 0; j < apartment.Numbers[i].NumberEntities.Count; j++)
				{
					apartment.Numbers[i].NumberEntities[j].Reservations = new List<Reservation>();
					apartment.Numbers[i].NumberEntities[j].EntityReservations = _context.EntityReservations.Where(o => o.NumberEntityId == apartment.Numbers[i].NumberEntities[j].Id).ToList();

					for (int t = 0; t < apartment.Numbers[i].NumberEntities[j].EntityReservations.Count; t++)
						if (!apartment.Numbers[i].NumberEntities[j].Reservations.Any(o => o.Id == apartment.Numbers[i].NumberEntities[j].EntityReservations[t].ReservationId))
							apartment.Numbers[i].NumberEntities[j].Reservations.Add(_context.Reservations.Find(apartment.Numbers[i].NumberEntities[j].EntityReservations[t].ReservationId));
				}

			}

			apartment.Numbers = apartment.Numbers.Where(o => o.Packs.Count() > 0 && o.NumberEntities.Count() > 0).ToList();

			return apartment;
		}

		public async Task<IActionResult> Index(int page = 1, BoolEnum finished = BoolEnum.All, BoolEnum enabled = BoolEnum.All, int countryId = 0, int cityId = 0, SortApartments sortOrder = SortApartments.NameAsc, string name = "")
		{
			User user = await _userManager.GetUserAsync(User);
			ApartmentsViewModel model = new ApartmentsViewModel()
			{
				FilterModel = new FilterApartmentsViewModel()
				{
					Finished = finished, 
					Enabled = enabled,
					CountryId = countryId,
					CityId = cityId,
					Name = name,
				},
				SortOrder = new SortApartmentsViewModel(sortOrder)
			};
			model.Apartments = _context.Apartments.Where(o => o.UserId == user.Id)
				.Include(o => o.Country)
				.Include(o => o.City)
				.Include(o => o.ApartmentType)
				.ToList();

			model.FilterName();

			if (cityId > 0)
				model.Apartments = model.Apartments.Where(o => o.CityId == cityId).ToList();

			if (countryId > 0)
				model.Apartments = model.Apartments.Where(o => o.CountryId == countryId).ToList();

			switch (finished)
			{
				case BoolEnum.FalseOnly:
					model.Apartments = model.Apartments.Where(o => o.Finished == false).ToList();
					break;
				case BoolEnum.TrueOnly:
					model.Apartments = model.Apartments.Where(o => o.Finished == true).ToList();
					break;
			}

			switch (enabled)
			{
				case BoolEnum.FalseOnly:
					model.Apartments = model.Apartments.Where(o => o.Enable == false).ToList();
					break;
				case BoolEnum.TrueOnly:
					model.Apartments = model.Apartments.Where(o => o.Enable == true).ToList();
					break;
			}

			model.Page = new PageViewModel(model.Apartments.Count, page, 10);
			model.Sort();
			model.Cut();


			ViewData["Countries"] = new SelectList(_context.Countries.ToArray(), "Id", "Name", model.FilterModel.CountryId);
			ViewData["Cities"] = new SelectList(_context.Cities.Where(o => o.CountryId == model.FilterModel.CountryId).ToArray(), "Id", "Name", model.FilterModel.CityId);
			return View(model);
		}

		[HttpGet]
		public ActionResult Search(DateTime? begin, DateTime? end, int cityId = 0, int countryId = 0, string name = "", int page = 1, SortApartments sortOrder = SortApartments.NameAsc, int apartmentTypeId = 0)
		{
			var model = new ApartmentsViewModel();
			model.FilterModel = new FilterApartmentsViewModel()
			{
				ReservationTimeBegin = begin > end ? end : begin,
				ReservationTimeEnd = end > begin ? end: begin,
				CityId = cityId,
				CountryId = countryId == 0 && cityId > 0? _context.Countries.Include(o => o.Cities).First(o => o.Cities.Contains(_context.Cities.Find((int)cityId))).Id : countryId,
				ApartmentTypeId = apartmentTypeId,
				Name = name
			};
			model.SortOrder = new SortApartmentsViewModel(sortOrder);

			model.Apartments = _context.Apartments.Where(o => o.Enable && o.Finished).ToList();

			if (countryId > 0)
				model.Apartments = model.Apartments.Where(o => o.CountryId == countryId).ToList();

			if (cityId > 0)
				model.Apartments = model.Apartments.Where(o => o.CityId == cityId).ToList();

			if (apartmentTypeId > 0)
				model.Apartments = model.Apartments.Where(o => o.ApartmentTypeId == apartmentTypeId).ToList();

			model.FilterName();

			for(int i = 0; i < model.Apartments.Count; i++)
			{
				model.Apartments[i].Numbers = _context.Numbers.Where(o => o.ApartmentId == model.Apartments[i].Id).Include(o => o.NumberEntities).ToList();
				model.Apartments[i].Reservations = _context.Reservations.Where(o => o.ApartmentId == model.Apartments[i].Id).Include(o => o.EntityReservations).ToList();
				model.Apartments[i].Reviews = _context.Reviews.Where(o => o.ApartmentId == model.Apartments[i].Id).ToList();
			}

			if (begin != null && end != null)
				model.Apartments = model.Apartments.Where(o => o.HaveFreeNumbers(model.FilterModel.ReservationTimeBegin, model.FilterModel.ReservationTimeEnd)).ToList();

			model.Page = new PageViewModel(model.Apartments.Count, page, 10);
			model.Sort();
			model.Cut();

			for (int i = 0; i < model.Apartments.Count; i++)
			{
				model.Apartments[i].Photos = _context.Photos.Where(o => o.ApartmentId == model.Apartments[i].Id).ToList();
				model.Apartments[i].ApartmentType = _context.ApartmentTypes.Find(model.Apartments[i].ApartmentTypeId);
				model.Apartments[i].Country = _context.Countries.Find(model.Apartments[i].CountryId);
				model.Apartments[i].City = _context.Cities.Find(model.Apartments[i].CityId);
			}

			ViewBag.ApartmentTypes = new SelectList(_context.ApartmentTypes.ToArray(), "Id", "Name", model.FilterModel.ApartmentTypeId);
			ViewData["Countries"] = new SelectList(_context.Countries.ToArray(), "Id", "Name", model.FilterModel.CountryId);
			ViewData["Cities"] = new SelectList(_context.Cities.Where(o => o.CountryId == model.FilterModel.CountryId).ToArray(), "Id", "Name", model.FilterModel.CityId);
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
			model.Apartment = GetApartment(id);

			return View(model);
		}

		[HttpPost]
		public ActionResult SearchNumber(ApartmentDetailsViewModel model)
		{
			model.MinDate = DateTime.Now;
			model.MaxDate = DateTime.Now.AddDays(90);

			if(DateTime.Compare(model.Begin, model.End) >= 0)
			{
				ModelState.AddModelError("End", "Дата начала бронирования не может превышать дату его конца");
				model.Apartment = GetApartment(model.Apartment.Id);
			} 
			else
			{
				model.EnableReservation = true;
				model.Apartment = GetApartment(model.Apartment.Id);

				if (!string.IsNullOrEmpty(model.Name))
					model.Apartment.Numbers = model.Apartment.Numbers.Where(o => o.ApartmentId == model.Apartment.Id && o.Name.Contains(model.Name)).ToList();

				for (int i = 0; i < model.Apartment.Numbers.Count; i++)
				{
					model.Apartment.Numbers[i].NumberEntities = model.Apartment.Numbers[i].NumberEntities.Where(o => o.Reservations.All(r =>
							   r.ReservationBegin < model.Begin &&
							   r.ReservationEnd < model.Begin ||
							   r.ReservationBegin > model.End &&
							   r.ReservationEnd > model.End &&
							   !r.Cencel
					)).ToList();
				}
				model.Apartment.Numbers.RemoveAll(o => o.NumberEntities.Count == 0);
			}
			

			return View("Details", model);
		}

		[HttpGet]
		public async Task<ActionResult> Price(int id)
		{
			if (_context.PackTenants.Any(o => o.Id == id))
			{
				var packTenant = await _context.PackTenants
					.Include(o => o.Pack)
						.ThenInclude(o => o.PackServices)
							.ThenInclude(o => o.NumberService)
					.FirstAsync(o => o.Id == id);
				return Json(packTenant.Price + packTenant.Pack.PackServices.Sum(o => o.NumberService.Price));
			}
			return Json(0);
		}

		[HttpGet]
		public async Task<ActionResult> Reviews(int id, int page = 1)
		{
			var reviews = await _context.Reviews
				.Include(o => o.Reservation)
					.ThenInclude(o => o.User)
				.Include(o => o.ReviewScores)
				.Where(o => o.Apartment.Id == id && !string.IsNullOrEmpty(o.Message)).ToListAsync();
		
			var model = new ReviewsListViewModel()
			{
				Apartment = _context.Apartments.Find(id),
				PageViewModel = new PageViewModel(reviews.Count(), page, 10),
				Reviews = reviews,
				Scores = _context.Scores.ToList()
			};

			return View(model);
		}
	}
}
