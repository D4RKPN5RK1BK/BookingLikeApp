using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Enums;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "staff")]
	public class ApartmentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ApartmentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult Index(string name = "", int countryId = 0, int cityId = 0, int page = 1, BoolEnum finished = BoolEnum.All, BoolEnum enabled = BoolEnum.All, BoolEnum check = BoolEnum.All, SortApartments sortOrder = SortApartments.NameAsc)
		{
			ApartmentsViewModel model = new ApartmentsViewModel()
			{
				Apartments = _context.Apartments.ToList(),
				FilterModel = new FilterApartmentsViewModel()
				{
					Name = name,
					CountryId = countryId,
					CityId = cityId,
					Finished = finished,
					Enabled = enabled,
					Checked = check
				},
				SortOrder = new SortApartmentsViewModel(sortOrder),
			};
			

			if (countryId > 0)
				model.Apartments = model.Apartments.Where(o => o.CountryId == countryId).ToList();

			if (cityId > 0)
				model.Apartments = model.Apartments.Where(o => o.CityId == cityId).ToList();

			if (!string.IsNullOrEmpty(name))
			{
				if(!_context.Apartments.Any(o => o.Name.Contains(name)))
					model.Apartments = model.Apartments.Where(o => o.Name.ToLower() == name.ToLower()).ToList();
				else
					model.Apartments = model.Apartments.Where(o => o.Name.ToLower().Contains(name.ToLower())).ToList();
			}
				
			
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

			switch (check)
			{
				case BoolEnum.FalseOnly:
					model.Apartments = model.Apartments.Where(o => o.Checked == false).ToList();
					break;
				case BoolEnum.TrueOnly:
					model.Apartments = model.Apartments.Where(o => o.Checked == true).ToList();
					break;
			}

			for (int i = 0; i < model.Apartments.Count; i++)
			{
				model.Apartments[i].Reservations = _context.Reservations.Where(o => o.ApartmentId == model.Apartments[i].Id).ToList();
				model.Apartments[i].Reviews = _context.Reviews.Where(o => o.ApartmentId == model.Apartments[i].Id)
					.Include(o => o.ReviewScores)
					.ToList();
			}

			model.Sort();
			model.Page = new PageViewModel(model.Apartments.Count, page, 20);
			model.Cut();

			ViewData["Countries"] = new SelectList(_context.Countries.ToArray(), "Id", "Name", model.FilterModel.CountryId);
			ViewData["Cities"] = new SelectList(_context.Cities.Where(o => o.CountryId == model.FilterModel.CountryId).ToArray(), "Id", "Name", model.FilterModel.CityId);
			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> Read(int id)
		{
			if(_context.Apartments.Any(o => o.Id == id))
			{
				Models.Apartment model = await _context.Apartments
					.Include(o => o.User)
					.Include(o => o.ApartmentType)
					.Include(o => o.ApartmentServices)
					.Include(o => o.Country)
					.Include(o => o.City)
					.FirstAsync(o => o.Id == id);

				model.Numbers = await _context.Numbers
					.Include(o => o.Packs)
						.ThenInclude(o => o.PackServices)
							.ThenInclude(o => o.NumberService)
					.Include(o => o.Packs)
						.ThenInclude(o => o.PackTenants)
					.Where(o => o.ApartmentId == model.Id)
					.ToListAsync();
				return View(model);
			}
			
			return NotFound();
		}

		[HttpPost]
		public ActionResult Block(int id)
		{
			Models.Apartment apartment = _context.Apartments.Find(id);
			apartment.Bolcked = !apartment.Bolcked;
			_context.Update(apartment);
			_context.SaveChanges();
			return RedirectToAction("Read", new { id });
		}
		[HttpPost]
		public ActionResult Check(int id)
		{
			Models.Apartment apartment = _context.Apartments.Find(id);
			apartment.Checked = true;
			_context.Update(apartment);
			_context.SaveChanges();
			return RedirectToAction("Read", new { id });
		}
	}
}
