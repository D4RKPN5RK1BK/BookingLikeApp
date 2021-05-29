using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "staff")]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
