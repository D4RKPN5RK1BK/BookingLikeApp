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
using BookingLikeApp.Areas.Apartment.ViewModels;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Apartment.Controllers
{

	// Классы для формирования запроса 
	public class PackData
	{
		public int Count { get; set; }
		public int PackId { get; set; }
		public int PackTenantId { get; set; }
	}

	public class CreateRequest
	{
		public DateTime ReservationBegin { get; set; }
		public DateTime ReservationEnd { get; set; }
		public List<PackData> Packs { get; set; }
	}

	// Класс для формирования ответа
	class CreateResponce
	{
		public string Exception { get; set; }
		public string InnerException { get; set; }
		public List<string> Messages { get; set; }
		public int ReservationId { get; set; }
		public bool Success { get; set; }

		public CreateResponce()
		{
			Messages = new List<string>();
			ReservationId = 0;
			Success = false;
		}
	}

	// Контроллер
	[Authorize]
	[Area("apartment")]
	public class ReservationController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<User> _userManager;
		public ReservationController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<ActionResult> Index(int page = 1)
		{
			int pageSize = 10;

			ReservationsViewModel model = new ReservationsViewModel();
			model.ReservationDataPacks = new List<ReservationDataPack>();
			User user = await _userManager.GetUserAsync(User);
			List<Reservation> reservations = _context.Reservations.Where(o => o.UserId == user.Id && !o.Cencel).Skip((page - 1) * pageSize).Take(pageSize).ToList();

			foreach(var r in reservations)
			{
				EntityReservation entityReservation = _context.EntityReservations.FirstOrDefault(o => o.ReservationId == r.Id);
				Models.Apartment apartment = _context.Apartments.Find(_context.Numbers.Find(_context.Packs.Find(_context.PackTenants.Find(entityReservation.PackTenantId).PackId).NumberId).ApartmentId);
				model.ReservationDataPacks.Add(new ReservationDataPack()
				{
					Reservation = r,
					Apartment = apartment
				});

			}

			int count = _context.Reservations.Where(o => o.UserId == user.Id && !o.Cencel).Count();
			model.PageInfo = new PageViewModel(count, page, pageSize); 
			return View(model);
		}

		[HttpPost]
		public ActionResult Create([FromBody]CreateRequest model)
		{

			CreateResponce responce = new CreateResponce();


			//Часть с проверкой на корректность запроса
			
			try
			{
				/*return Json(model);*/
			}
			catch(Exception ex)
			{
				return Json("exception: " + ex.Message);
			}

			//Часть c созданием действительных обхектов и занесением их данных в базу данных

			try
			{
				Reservation reservation = new Reservation()
				{
					ReservationBegin = model.ReservationBegin,
					ReservationEnd = model.ReservationEnd,
					EntityReservations = new List<EntityReservation>(),
					TimeStamp = DateTime.Now,
					UserId = _userManager.GetUserId(User),
				};

				_context.Add(reservation);

				foreach (var p in model.Packs)
				{
					List<Reservation> reservations = new List<Reservation>();
					Number number = _context.Numbers.Find(_context.Packs.Find(p.PackId).NumberId);

					number.NumberEntities = _context.NumberEntities.Where(o => o.NumberId == number.Id).ToList();
					for (int i = 0; i < number.NumberEntities.Count; i++)
					{
						number.NumberEntities[i].EntityReservations = _context.EntityReservations.Where(o => o.NumberEntityId == number.NumberEntities[i].Id).ToList();
						number.NumberEntities[i].Reservations = new List<Reservation>();
						for (int j = 0; j < number.NumberEntities[i].EntityReservations.Count; j++)
							if (!number.NumberEntities[i].Reservations.Any(o => o.Id == number.NumberEntities[i].EntityReservations[j].ReservationId))
								number.NumberEntities[i].Reservations.Add(_context.Reservations.Find(number.NumberEntities[i].EntityReservations[j].ReservationId));
					}


					for (int i = 0; i < p.Count; i++)
					{

						NumberEntity numberEntity = number.NumberEntities.FirstOrDefault(o => o.Reservations.All(r =>
							r.ReservationBegin < reservation.ReservationBegin &&
							r.ReservationEnd < reservation.ReservationBegin ||
							r.ReservationBegin > reservation.ReservationEnd &&
							r.ReservationEnd > reservation.ReservationEnd &&
							!r.Cencel
						));


						//Желательно сюда поставить исключение 
						if (numberEntity == null)
						{
							responce.Exception = "Все сущности заняты";
							responce.Messages.Add("Кто-то уже зарезервировал номра, перезагрузите страницу для обновления данных.");
							return Json(responce);
						}
						else
						{
							numberEntity.Reservations.Add(reservation);
							reservation.EntityReservations.Add(new EntityReservation()
							{
								PackTenantId = p.PackTenantId,
								NumberEntityId = numberEntity.Id,
								ReservationId = reservation.Id
							});
						}
					}
				}
				reservation.Price = 0;
				for(int i = 0; i < reservation.EntityReservations.Count; i++)
				{
					PackTenant packTenant = _context.PackTenants.Find(reservation.EntityReservations[i].PackTenantId);
					packTenant.Pack = _context.Packs.Find(packTenant.PackId);
					packTenant.Pack.PackServices = _context.PackServices.Where(o => o.PackId == packTenant.PackId).ToList();


					List<NumberService> numberServices = new List<NumberService>();
					for(int j = 0; j < packTenant.Pack.PackServices.Count; j++)
					{
						numberServices.Add(_context.NumberServices.Find(packTenant.Pack.PackServices[j].NumberServiceId));
					}

					reservation.Price += packTenant.Price;
					reservation.Price += (decimal)numberServices.Sum(o => o.Price);
				}

				if (reservation.EntityReservations.Count > 0)
				{
					_context.AddRange(reservation.EntityReservations);
					_context.SaveChanges();
					responce.Success = true;
					responce.ReservationId = reservation.Id;
					return Json(responce);
				}

			}
			catch (Exception ex)
			{
				responce.Exception = ex.Message;
				responce.InnerException = ex.InnerException.Message;
				return Json(responce);
			}

			return Json(responce);
			
		}

		[HttpGet]
		public ActionResult Read(int id)
		{
			ReservationReadViewModel model = new ReservationReadViewModel();
			model.ERDataPacks = new List<EntityReservationDataPack>();
			model.Reservation = _context.Reservations.Find(id);
			model.Reservation.EntityReservations = _context.EntityReservations.Where(o => o.ReservationId == model.Reservation.Id).ToList();

			foreach(var er in model.Reservation.EntityReservations)
			{
				if (!model.ERDataPacks.Any(o => o.EntityReservation.PackTenantId == er.PackTenantId))
				{
					EntityReservationDataPack ERDataPack = new EntityReservationDataPack()
					{
						EntityReservation = er,
						PackTenant = _context.PackTenants.Find(er.PackTenantId),
						Pack = _context.Packs.Find(_context.PackTenants.Find(er.PackTenantId).PackId),
						Number = _context.Numbers.Find(_context.Packs.Find(_context.PackTenants.Find(er.PackTenantId).PackId).NumberId),
						Count = _context.EntityReservations.Where(o => o.PackTenantId == er.PackTenantId && o.ReservationId == model.Reservation.Id).Count()
					};
					model.ERDataPacks.Add(ERDataPack);
				}
			}

			model.Apartment = _context.Apartments.Find(model.ERDataPacks.First().Number.ApartmentId);

			return View(model);

		}

		[HttpPost]
		public ActionResult Cencel(int id)
		{
			Reservation reservation = _context.Reservations.Find(id);
			reservation.Cencel = true;
			_context.Update(reservation);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Confirm(int id)
		{
			Reservation reservation = _context.Reservations.Find(id);
			reservation.Confirm = true;
			_context.Update(reservation);
			_context.SaveChanges();
			return RedirectToAction("Read", new { id = id });
		}
	}
}
