using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Enums;
using BookingLikeApp.Models;

namespace BookingLikeApp.ViewModels
{
	public class ReservationsViewModel
	{
		public List<Reservation> Reservations { get; set; }
		public ReservationsFilter FilterModel { get; set; }
		public ReservationsSort SortModel { get; set; }
		public PageViewModel PageModel { get; set; }

		public ReservationsViewModel()
		{
			FilterModel = new ReservationsFilter()
			{
				UserId = null,
				Accept = BoolEnum.All,
				Cancel = BoolEnum.All
			};
			SortModel = new ReservationsSort(ReservationSortEnum.ByDateAsc); 
		}

		public void Sort()
		{
			switch (SortModel.Current)
			{
				case ReservationSortEnum.ByDateAsc:
					Reservations = Reservations.OrderBy(o => o.TimeStamp).ToList();
					break;
				case ReservationSortEnum.ByDateDesc:
					Reservations = Reservations.OrderByDescending(o => o.TimeStamp).ToList();
					break;
				case ReservationSortEnum.ByPriceAsc:
					Reservations = Reservations.OrderBy(o => o.Price).ToList();
					break;
				case ReservationSortEnum.ByPriceDesc:
					Reservations = Reservations.OrderByDescending(o => o.Price).ToList();
					break;
				default:
					Reservations = Reservations.OrderBy(o => o.TimeStamp).ToList();
					break;
			}
		}

		public void Cut() => 
			Reservations = Reservations.Skip(PageModel.PageNumber * PageModel.PageSize - PageModel.PageSize).Take(PageModel.PageSize).ToList();
	}

	public class ReservationsFilter
	{
		public string UserId { get; set; }
		public int ApartmentId { get; set; }
		public BoolEnum Accept { get; set; }
		public BoolEnum Cancel { get; set; }
	}

	public enum ReservationSortEnum
	{
		ByDateAsc,
		ByDateDesc,
		ByPriceAsc,
		ByPriceDesc,
	}

	public class ReservationsSort
	{
		public ReservationSortEnum Date { get; set; }
		public ReservationSortEnum Current { get; set; }

		public ReservationsSort(ReservationSortEnum sortOrder)
		{
			Date = sortOrder == ReservationSortEnum.ByDateAsc ? ReservationSortEnum.ByDateAsc : ReservationSortEnum.ByDateDesc;
			Current = sortOrder;
		}
	}
}
