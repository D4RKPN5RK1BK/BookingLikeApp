using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Apartment.Controllers
{
	[Area("apartment")]
	public class PackController : Controller
	{
		//Общие данные
		protected readonly ApplicationDbContext _context;

		public PackController(ApplicationDbContext context)
		{
			_context = context;
		}

		//Pack

		[HttpPost]
		public async Task<ActionResult> Create([FromBody]int id)
		{
			Pack pack = new Pack()
			{
				NumberId = id,
				Enable = true,
				Name = "Пакет услуг"
			};

			await _context.Packs.AddAsync(pack);
			await _context.SaveChangesAsync();
			return Json(pack);
		}

		[HttpPut]
		public async Task Update([FromBody]Pack model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		[HttpDelete]
		public async Task Delete([FromBody]int id)
		{
			_context.Remove(await _context.Packs.FindAsync(id));
			await _context.SaveChangesAsync();
		}

		//NumberService

		[HttpGet]
		public ActionResult ServicesList(int id)
		{
			List<NumberService> model = _context.NumberServices.Where(o => o.NumberId == id).ToList();
			foreach (var i in model)
			{
				i.Number = null;
				i.PackServices = null;
			}
			return Json(model);
		}

		//PackService

		[HttpPost]
		public async Task CreateService([FromBody]PackService model)
		{

			await _context.AddAsync(model);
			await _context.SaveChangesAsync();
		}

		[HttpDelete]
		public void DeleteService([FromBody]PackService model)
		{
			_context.RemoveRange(_context.PackServices.Where(o => o.PackId == model.PackId && o.NumberServiceId == model.NumberServiceId));
			_context.SaveChanges();
		}

		//PackTenant

		[HttpPost]
		public ActionResult CreatePackTenant([FromBody]int id)
		{
			PackTenant packTenant = new PackTenant()
			{
				PackId = id,
			};

			_context.Add(packTenant);
			_context.SaveChanges();

			packTenant.Pack = null;

			return Json(packTenant);
		}

		[HttpDelete]
		public async Task DeletePackTenant([FromBody]int id)
		{
			PackTenant packTenant = await _context.PackTenants.FindAsync(id);
			_context.Remove(packTenant);
			_context.SaveChanges();
		}

		[HttpPut]
		public void UpdatePackTenant([FromBody]PackTenant model)
		{
			_context.Update(model);
			_context.SaveChanges();
		}
	}
}
