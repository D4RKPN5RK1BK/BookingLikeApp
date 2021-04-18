using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class UserApartmentsViewModel
	{
		public List<Models.Apartment> Apartments { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
