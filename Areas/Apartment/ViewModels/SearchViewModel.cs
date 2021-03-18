using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class SearchViewModel
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public DateTime Begin { get; set; }
		public DateTime End { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
	}
}
