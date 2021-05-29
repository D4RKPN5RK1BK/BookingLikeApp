using System.Linq;
using BookingLikeApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingLikeApp.Controllers
{
	public class LocationController : Controller
	{
		private readonly ApplicationDbContext _context;
		public LocationController(ApplicationDbContext context)
		{
			_context = context;
		}

		public ActionResult CountrySelect(int id = 0)
		{
			ViewBag.SelectList = new SelectList(_context.Countries.ToList(), "Id", "Name", id);
			return PartialView("_CountrySelect");
		}

		public ActionResult CitySelect(int id = 0, int select = 0)
		{
			var model = new SelectList(_context.Cities.Where(o => o.CountryId == id).ToList(), "Id", "Name", select);
			return PartialView("_CitySelect", model);
		}
	}
}
