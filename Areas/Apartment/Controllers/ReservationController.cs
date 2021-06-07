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
using BookingLikeApp.ViewModels;
using BookingLikeApp.Enums;
using Microsoft.EntityFrameworkCore;

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
		public string Message { get; set; }
		public string Target { get; set; }

		public int ReservationId { get; set; }
		public bool Success { get; set; }
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
		public ActionResult Index(int page = 1, int apartmentId = 0, BoolEnum accept = BoolEnum.All, ReservationSortEnum orderBy = ReservationSortEnum.ByDateDesc)
		{
			var model = new ReservationsViewModel();
			model.FilterModel = new ReservationsFilter()
			{
				Accept = accept,
				ApartmentId = apartmentId,
				UserId = _userManager.GetUserId(User)
			};
			model.SortModel = new ReservationsSort(orderBy);

			model.Reservations = _context.Reservations.Where(o => o.UserId == model.FilterModel.UserId && !o.Cencel).ToList();

			if (apartmentId > 0)
				model.Reservations = model.Reservations.Where(o => o.ApartmentId == apartmentId).ToList();

			switch (accept)
			{
				case BoolEnum.TrueOnly:
					model.Reservations = model.Reservations.Where(o => o.Confirm).ToList();
					break;
				case BoolEnum.FalseOnly:
					model.Reservations = model.Reservations.Where(o => !o.Confirm).ToList();
					break;
			}

			model.PageModel = new PageViewModel(model.Reservations.Count, page, 10);
			model.Sort();
			model.Cut();

			for(int i = 0; i < model.Reservations.Count; i++)
				model.Reservations[i].Apartment = _context.Apartments.Find(model.Reservations[i].ApartmentId);

			return View(model);
		}

		[HttpPost]
		public ActionResult Create([FromBody]CreateRequest model)
		{

			CreateResponce responce = new CreateResponce() { Success = false };


			//Часть с проверкой на корректность запроса
			
			try
			{


				if(model.Packs == null || model.Packs.Count == 0 || model.Packs.Sum(o => o.Count) == 0)
				{
					responce.Message = "Не выбран ни один номер";
					responce.Target = "";
					return Json(responce);
				}

				model.Packs.RemoveAll(o => o.Count <= 0 || o.PackTenantId == 0 || o.PackId == 0);

				if(model.ReservationBegin >= model.ReservationEnd)
				{
					responce.Message = "Некорректный промежуток времени";
					return Json(responce);
				}

				/*if(model.Packs.Any())*/
			}
			catch(NullReferenceException ex)
			{
				responce.Exception = ex.Message;
				responce.InnerException = ex.InnerException.Message;
				responce.Message = "Некорректно составленный запрос";
				return Json(responce);
			}
			catch(Exception ex)
			{
				responce.Exception = ex.Message;
				responce.Message = "Не выбар ни один элемент";
				responce.Success = false;
			}

			//Часть c созданием действительных обхектов и занесением их в базу данных

			try
			{
				Reservation reservation = new Reservation()
				{
					ReservationBegin = model.ReservationBegin,
					ReservationEnd = model.ReservationEnd,
					EntityReservations = new List<EntityReservation>(),
					TimeStamp = DateTime.Now,
					UserId = _userManager.GetUserId(User),
					ApartmentId = _context.Numbers.Find(_context.Packs.Find(model.Packs.First().PackId).NumberId).ApartmentId
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
							responce.Message = "Кто-то уже зарезервировал номра, перезагрузите страницу для обновления данных.";
							responce.Target = "";
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
					decimal tempPrice = 0;

					tempPrice += packTenant.Price;
					tempPrice += (decimal)numberServices.Sum(o => o.Price);
					tempPrice *= (decimal)(reservation.ReservationEnd - reservation.ReservationBegin).TotalDays + 1;
					reservation.Price += tempPrice;
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
			catch
			{
				responce.Success = false;
				responce.Message = "Невозможно обработать запрос";
				return Json(responce);
			}

			return Json(responce);
			
		}

		[HttpGet]
		public async Task<ActionResult> Info(int id)
		{
			User user = await _userManager.GetUserAsync(User);
			var model = new ReservationReadViewModel()
			{
				ERDataPacks = new List<EntityReservationDataPack>(),
				Reservation = await _context.Reservations
					.Include(o => o.EntityReservations)
					.Include(o => o.Apartment)
					.Include(o => o.Review)
					.FirstAsync(o => o.Id == id),
			};
			model.AllowAccept = (user.Wallet - model.Reservation.Price > 0); 

			foreach(var er in model.Reservation.EntityReservations)
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

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Cencel(int id)
		{
			Reservation reservation = await _context.Reservations.Include(o => o.Review).FirstAsync(o => o.Id == id);
			reservation.Cencel = true;
			_context.Update(reservation);
			if (reservation.Review != null)
				_context.Remove(reservation.Review);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<ActionResult> Confirm(int id)
		{
			Reservation reservation = _context.Reservations.Find(id);
			reservation.Transaction = new Transaction()
			{
				Value = reservation.Price,
				InputUser = await _userManager.GetUserAsync(User),
				OutputUser = await _context.Users.Include(o => o.Apartments).FirstAsync(o => o.Apartments.Any(o => o.Id == reservation.ApartmentId)),
				TimeStamp = DateTime.Now
			};

			if ((reservation.Transaction.InputUser.Wallet -= reservation.Transaction.Value) >= 0)
			{
				if (reservation.Transaction.InputUser.Id != reservation.Transaction.OutputUser.Id)
				{
					reservation.Transaction.InputUser.Wallet -= reservation.Transaction.Value;
					reservation.Transaction.OutputUser.Wallet += reservation.Transaction.Value;
				}
				reservation.Confirm = true;
				_context.Update(reservation);
				_context.Update(reservation.Transaction);
				_context.Update(reservation.Transaction.InputUser);
				_context.Update(reservation.Transaction.OutputUser);
				_context.SaveChanges();
			}

			
			return RedirectToAction("Info", new { id = id });
		}

		//Отзывы


		[HttpGet]
		public async Task<ActionResult> CreateReview(int id)
		{
			var model = new ReservationReadViewModel()
			{
				ERDataPacks = new List<EntityReservationDataPack>(),
				Scores = _context.Scores.ToList(),
				Reservation = await _context.Reservations
					.Include(o => o.Apartment)
					.Include(o => o.Review)
					.Include(o => o.EntityReservations)
					.FirstAsync(o => o.Id == id)
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult CreateReview(Review model)
		{
			model.CreateAt = DateTime.Now;
			if(model.ReviewScores != null)
				model.ReviewScores.RemoveAll(o => o.Value <= 0 || o.Value > _context.Scores.Find(o.ScoreId).MaxValue);
			_context.Add(model);
			_context.SaveChanges();
			return RedirectToAction("UpdateReview", new {id = model.ReservationId});
		}

		[HttpGet]
		public async Task<ActionResult> UpdateReview(int id, bool success = false)
		{
			ReservationReadViewModel model = new ReservationReadViewModel()
			{
				ERDataPacks = new List<EntityReservationDataPack>(),
				Scores = _context.Scores.ToList(),
				Reservation = await _context.Reservations
					.Include(o => o.Apartment)
					.Include(o => o.Review)
						.ThenInclude(o => o.ReviewScores)
					.Include(o => o.EntityReservations)
					.FirstAsync(o => o.Id == id)
			};
			
			if (success)
				ViewData["Message"] = "Данные успешно обновлены";

			return View(model);
		}
		[HttpPost]
		public ActionResult UpdateReview(Review model)
		{
			if(ModelState.IsValid)
			{
				model.CreateAt = DateTime.Now;
				_context.ReviewScores.RemoveRange(_context.ReviewScores.Where(o => o.ReviewId == model.ReservationId));
				if (model.ReviewScores != null)
					model.ReviewScores.RemoveAll(o => o.Value <= 0 || o.Value > _context.Scores.Find(o.ScoreId).MaxValue);
				model.ApartmentId = _context.Reservations.Find(model.ReservationId).ApartmentId;
				var result = _context.Update(model);
				_context.SaveChanges();
				return RedirectToAction("Info", new { id = model.ReservationId });
			}
			else
			{
				return View(model);
			}
		}

		[HttpPost]
		public ActionResult DeleteReview(int id)
		{
			_context.Reviews.Remove(_context.Reviews.Find(id));
			_context.SaveChanges();
			return RedirectToAction("Info", new { id = id});
		}

		[HttpGet]
		public async Task<ActionResult> Read(int id)
		{
			Reservation model = await _context.Reservations
				.Include(o => o.User)
				.Include(o => o.Apartment)					
				.FirstAsync(o => o.Id == id);

			model.EntityReservations = await _context.EntityReservations
				.Include(o => o.PackTenant)
					.ThenInclude(o => o.Pack)
				.Include(o => o.NumberEntity)
				.Where(o => o.ReservationId == id)
				.ToListAsync();

			return View(model);
		}
	}
}


