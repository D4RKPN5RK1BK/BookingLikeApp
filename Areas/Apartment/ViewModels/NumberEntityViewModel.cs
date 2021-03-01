using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class NumberEntityViewModel
	{
		public List<DateTime> Dates { get; set; }
		public List<NumberEntity> Entities { get; set; }
	}
}
