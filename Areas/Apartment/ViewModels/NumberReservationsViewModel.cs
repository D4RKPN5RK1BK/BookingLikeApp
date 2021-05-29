using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class NumberReservationsViewModel : Number
	{
		public NumberReservationsFilter FilterModel { get; set; }
		public List<Reservation> Reservations { get; set; }
		public List<DateTime> Dates { get; set; }
	}

	public class NumberReservationsFilter
	{
		public DateTime Begin { get; set; }
		public DateTime End { get; set; }
	}
}
