using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingLikeApp.ViewModels
{
	public class IndexViewModel
	{
		public SelectList Countries { get; set; }
		public List<Country> CountriesPopular { get; set; }
		public List<City> CitiesPopular { get; set; }
		public List<ApartmentType> ApartmentTypesPopular { get; set; }
		public List<Apartment> ApartmentsPopular { get; set; }

	}
}
