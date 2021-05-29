using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("Apartment")]
	public class DeleteController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;

		public DeleteController(ApplicationDbContext context, UserManager<User> userManger)
		{
			_context = context;
			_userManager = userManger;
		}

		[HttpGet]
		public async Task<ActionResult> Index(int id)
		{ 
			Models.Apartment model = await _context.Apartments
				.Include(o => o.Registration)
				.Include(o => o.Reservations)
				.FirstAsync(o => o.Id == id && o.UserId == _userManager.GetUserId(User));

			return model == null ? NotFound() : View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id)
		{
			Models.Apartment model = await _context.Apartments
				.Include(o => o.Registration)
				.FirstAsync(o => o.Id == id && o.UserId == _userManager.GetUserId(User));

			if (model == null)
				return NotFound();

			_context.Remove(model);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Read");
		}
	}
}
