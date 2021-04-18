using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class SearchApartmentViewModel
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public List<Models.Apartment> Apartments { get; set; }
		public PageViewModel PageModel { get; set; }
	}
}
