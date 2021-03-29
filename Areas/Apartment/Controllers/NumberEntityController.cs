using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("Apartment")]
	public class NumberEntityController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public NumberEntityController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public ActionResult Count([FromForm]int numberId, [FromForm]int count)
		{
			List<NumberEntity> entities = _context.NumberEntities.Where(o => o.NumberId == numberId).ToList();

			if (count < entities.Count)
			{
				entities = _context.NumberEntities.Where(o => o.NumberId == numberId).Skip(count).ToList();
				_context.NumberEntities.RemoveRange(entities);
			}
			else if(count > entities.Count)
			{
				int lastName = 1;
				if(entities.Count > 0)
					if (Int32.TryParse(entities.Last().Name, out lastName))
						lastName++;

				int newCount = count - entities.Count;
				entities.Clear();

				for (int i = 0; i < newCount; i++)
				{
					entities.Add(new NumberEntity() { Name = (lastName + i).ToString(), Enable = true, NumberId = numberId });
				}
				_context.AddRange(entities);
			}

			_context.SaveChanges();

			
			return RedirectToAction("Entities", "Number", new { id = numberId });
		}

		[HttpDelete]
		public void Delete([FromBody]int id)
		{
			_context.Remove(_context.NumberEntities.Find(id));
			_context.SaveChanges();
		}

		[HttpPut]
		public void Update([FromBody]NumberEntity model)
		{
			_context.Update(model);
			_context.SaveChanges();
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody]int numberId)
		{
			int name = 1;
			if (_context.NumberEntities.Any(o => o.NumberId == numberId))
				if(Int32.TryParse(_context.NumberEntities.Where(o => o.NumberId == numberId).OrderBy(o => o.Id).Last().Name, out name))
					name++;

			NumberEntity numberEntity = new NumberEntity() { NumberId = numberId, Enable = false, Name = name.ToString() };
			await _context.AddAsync(numberEntity);
			await _context.SaveChangesAsync();
			numberEntity.Number = null;
			return Json(numberEntity);
		}
	}
}
