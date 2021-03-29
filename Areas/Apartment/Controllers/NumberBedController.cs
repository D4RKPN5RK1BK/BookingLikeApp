using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Authorize]
	[Area("apartment")]
	public class NumberBedController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public NumberBedController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<JsonResult> Create([FromBody]int numberId)
		{
			NumberBed numberBed = new NumberBed() { BedId = _context.Beds.FirstOrDefault().Id, NumberId = numberId, Quantity = 0};
			_context.NumberBeds.Add(numberBed);
			await _context.SaveChangesAsync();

			numberBed.Bed = null;
			numberBed.Number = null;

			return Json(numberBed);
		}

		[HttpDelete]
		public async Task Delete([FromBody]int id)
		{
			NumberBed numberBed = await _context.NumberBeds.FindAsync(id);
			_context.Remove(numberBed);
			await _context.SaveChangesAsync();

		}



		[HttpPut]
		public async Task Update([FromBody] NumberBed numberBed)
		{
			_context.Update(numberBed);
			await _context.SaveChangesAsync();
		}

	}
}
