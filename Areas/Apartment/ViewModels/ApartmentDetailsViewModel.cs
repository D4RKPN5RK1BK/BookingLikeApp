using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class ApartmentDetailsViewModel
	{
		public string Name { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }

		public DateTime Begin { get; set; }
		public DateTime End { get; set; }

		public bool EnableReservation { get; set; }

		public Models.Apartment Apartment { get; set; }
		public List<EntityReservation> EntityReservations { get; set; }
	}
}
