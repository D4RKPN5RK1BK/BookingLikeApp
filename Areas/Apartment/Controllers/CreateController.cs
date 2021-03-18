using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
    public class CreateController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public CreateController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
			bool exist = _context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id);
			ViewData["Exist"] = exist;
			if (exist)
			{
				ViewData["Id"] = _context.Apartments.FirstOrDefault(o => !o.Finished).Id;
			}
            ViewBag.ApartmentTypes = _context.ApartmentTypes.ToList();
            return View();
        }

        public async Task<IActionResult> Initialize(int apartmentType) 
        {
            User user = await _userManager.GetUserAsync(User);
            Models.Apartment apartment;
            if (_context.Apartments.Any(o => o.Finished == false && o.UserId == user.Id))
            {
                apartment = _context.Apartments.FirstOrDefault(o => o.Finished == false && o.UserId == user.Id);
                _context.Remove(apartment);
            }
            
            apartment = new Models.Apartment() { UserId = user.Id, ApartmentTypeId = apartmentType , Name = _context.ApartmentTypes.Find(apartmentType).Name};
            Registration registration = new Registration() { Apartment = apartment };
            await _context.AddAsync(registration);
            await _context.AddAsync(apartment);
            await _context.SaveChangesAsync();

            return RedirectToAction("BasicInfo", "Edit", new { apartment.Id });
        }

       
    }
}
