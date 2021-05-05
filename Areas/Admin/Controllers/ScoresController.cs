using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize]
	public class ScoresController : Controller
	{
		protected readonly ApplicationDbContext _context;
		public ScoresController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index() => View(_context.Scores.ToList());

		[HttpPost]
		public async Task<ActionResult> Create(Score model)
		{
			_context.Add(model);
			return RedirectToAction("Index");
		}

		[HttpPut]
		public async Task Update([FromBody]Score model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			_context.Remove(_context.Scores.Find(id));
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
	}
}
