using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
    [Area("apartment")]
    public class CreateController : Controller
    {
        public IActionResult BasicInfo()
        {
            return View();
        }

        public IActionResult Numbers()
        {
            return View(); 
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Photos()
        {
            return View();
        }

        public IActionResult Facilites()
        {
            return View();
        }

    }
}
