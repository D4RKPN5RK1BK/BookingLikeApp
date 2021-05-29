using BookingLikeApp.Data;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BookingLikeApp.Controllers
{
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
			_context = context;
        }

		[HttpGet]
        public ActionResult Index()
        {
			IndexViewModel model = new IndexViewModel();
			model.Countries = new SelectList(_context.Countries.ToArray(), "Id", "Name");

			model.CountriesPopular = _context.Countries
				.Include(o => o.Apartments)
				.OrderByDescending(o => o.Apartments.Count)
				.Take(4)
				.ToList();

			model.CitiesPopular = _context.Cities
				.Include(o => o.Apartments)
				.Include(o => o.Country)
				.OrderByDescending(o => o.Apartments.Count)
				.Take(4)
				.ToList();
			
			model.ApartmentTypesPopular = _context.ApartmentTypes
				.Include(o => o.Apartments)
				.OrderByDescending(o => o.Apartments.Count)
				.Take(5)
				.ToList();
			
			model.ApartmentsPopular = _context.Apartments
				.Include(o => o.Reservations)
				.Include(o => o.Reviews)
					.ThenInclude(o => o.ReviewScores)
				.Include(o => o.Photos)
				.Take(100)
				.OrderByDescending(o => o.Reservations.Count)
				.ToList();
			
			model.ApartmentsPopular = model.ApartmentsPopular
				.Where(o => o.EnableToSearch)
				.OrderByDescending(o => o.Reservations.Count)
				.Take(8)
				.ToList();

			return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult About()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Secret() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
