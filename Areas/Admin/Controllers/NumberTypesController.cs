using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "staff")]
	public class NumberTypesController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public NumberTypesController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index() => View(_context.NumberTypes.ToList());

		public async Task<IActionResult> Edit(int id) => View(await _context.NumberTypes.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Edit(NumberType model)
		{
			if (ModelState.IsValid)
			{
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> Delete(int id) => View(await _context.NumberTypes.FindAsync(id));
		[HttpPost]
		public async Task<IActionResult> Delete(NumberType model)
		{
			_context.NumberTypes.Remove(model);
			await _context.SaveChangesAsync();
			return View();
		}

		public IActionResult Create() => View(new NumberType());
		[HttpPost]
		public async Task<IActionResult> Create(NumberType model)
		{
			if (ModelState.IsValid)
			{
				await _context.AddAsync(model);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
