using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
    [Area("account")]
    public class WalletsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
