using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
    [Area("account")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userMaanager;

        public HomeController(UserManager<User> userManager)
        {
            _userMaanager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            User model = await _userMaanager.GetUserAsync(User);
            return View(model);
        }
    }
}
