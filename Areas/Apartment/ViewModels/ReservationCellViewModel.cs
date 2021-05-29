using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class ReservationCellViewModel
	{
		public Reservation Reservation {get;set;}
		public DateTime Current { get; set; }
		/*public DateTime Begin { get; set; }*/
		public DateTime End { get; set; }

		public ReservationCellViewModel(Reservation reservation, DateTime current, DateTime end)
		{
			Reservation = reservation;
			Current = current;
			End = end;
		}

		/*public ReservationCellViewModel(Reservation reservation, DateTime begin, DateTime end)
		{
			Reservation = reservation;
			Begin = begin;
			End = end;
		}*/
	}
}
