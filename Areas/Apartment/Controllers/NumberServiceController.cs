using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("Apartment")]
	public class NumberServiceController : Controller
	{

		protected readonly ApplicationDbContext _context;

		public NumberServiceController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody]int numberId)
		{
			string name = string.Empty;
			if (_context.NumberServices.Any(o => o.NumberId == numberId))
			{
				List <NumberService> services = _context.NumberServices.Where(o => o.NumberId == numberId).ToList();

				for(int i = 1; i < int.MaxValue; i++)
				{
					if (!services.Any(o => o.Name == $"Услуга_{i}"))
					{
						name = $"Услуга_{i}";
						break;
					}
				}
			}
			else
			{
				name = "Услуга_1";
			}

			NumberService model = new NumberService()
			{
				Name = name,
				NumberId = numberId,
				HavePrice = false,
				Price = null
			};

			await _context.AddAsync(model);
			await _context.SaveChangesAsync();

			model.Number = null;
			model.Price = null;

			return Json(model);
		}

		[HttpPut]
		public async Task Update([FromBody]NumberService model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		[HttpDelete]
		public async Task Delete([FromBody]int id)
		{
			_context.NumberServices.Remove(await _context.NumberServices.FindAsync(id));
			await _context.SaveChangesAsync();
		}
	}
}
