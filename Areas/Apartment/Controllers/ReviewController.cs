using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	public class ReviewController : Controller
	{
		protected readonly ApplicationDbContext _context;
		public ReviewController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult> Create(Review model)
		{
			try
			{
				model.CreateAt = DateTime.Now;
				await _context.Reviews.AddAsync(model);
				await _context.SaveChangesAsync();
			}
			catch(NullReferenceException ex)
			{
				return Json(new { Message = ex.Message, Target = "", Success = false });
			}
			return Json(new { Message = "Обрзор успешно добавлен", Target = "", Success = true });
		}

		[HttpPut]
		public async Task<ActionResult> Update(Review model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
			return Json(new { Message = "Данные обновлены", Target = "", Success = true });
		}

		[HttpDelete]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				Review review = _context.Reviews.Find(id);
				_context.Remove(review);
				await _context.SaveChangesAsync();
			}
			catch(NullReferenceException ex)
			{
				return Json(new { Message = ex.Message, Target = "", Success = false });
			}
			return Json(new { Message = "Обрзор успешно удален", Target = "", Success = true });
		}
	}
}
