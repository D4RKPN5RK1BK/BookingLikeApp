using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class ReviewsListViewModel
	{
		public Models.Apartment Apartment { get; set; }
		public List<Review> Reviews { get; set; }
		public List<Score> Scores { get; set; }
		public PageViewModel PageViewModel { get; set; }

	}
}
