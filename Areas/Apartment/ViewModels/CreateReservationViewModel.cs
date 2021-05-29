using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class CreateReservationViewModel
	{
		public List<ReservationDataPack> ReservationDataPacks { get; set; }
		public PageViewModel PageInfo { get; set; } 
	}

	public class ReservationDataPack
	{
		public Reservation Reservation { get; set; }
		public Models.Apartment Apartment { get; set; }
	}
}
