using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

		public async Task<IActionResult> Index()
		{
			User user = await _userManager.GetUserAsync(User);
			List<Models.Apartment> model = _context.Apartments.Where(o => o.UserId == user.Id).ToList();

			for(int i = 0; i < model.Count; i++)
			{
				model[i].Numbers = _context.Numbers.Where(o => o.ApartmentId == model[i].Id).ToList();
			}
			return View(_context.Apartments.ToList());
		}

		public IActionResult SearchByName (string name)
		{
			List<Models.Apartment> apartments;
			if (_context.Apartments.Any(o => o.Name == name))
			{
				apartments = _context.Apartments.Where(o => o.Name == name).ToList();
			}
			else
			{
				apartments = _context.Apartments.Where(o => o.Name.Contains(name)).ToList();
			}

			foreach (var a in apartments)
			{
				a.ApartmentType = _context.ApartmentTypes.Find(a.ApartmentTypeId);
			}

			return View("Search", apartments);
		}

		public async Task<ActionResult> Search(SearchViewModel model)
		{
			return View("Search");
		}

		public IActionResult Details(int id)
		{
			Models.Apartment apartment = _context.Apartments.Find(id);
			apartment.ApartmentType = _context.ApartmentTypes.Find(apartment.ApartmentTypeId);
			apartment.Numbers = _context.Numbers.Where(o => o.ApartmentId == apartment.Id).ToList();
			apartment.ApartmentServices = _context.ApartmentServices.Where(o => o.ApartmentId == apartment.Id).ToList();
			foreach(var n in apartment.Numbers)
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
			return View(apartment);
		}
	}
}
