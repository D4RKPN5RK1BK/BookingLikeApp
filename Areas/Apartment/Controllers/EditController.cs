using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
    public class EditController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
