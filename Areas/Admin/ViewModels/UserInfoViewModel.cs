using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Admin.ViewModels
{
	public class UserInfoViewModel
	{
		public User User { get; set; }
		public List<string> UserRoles { get; set; }
		public ApartmentsViewModel SearchApartments { get; set; }
	}
}
