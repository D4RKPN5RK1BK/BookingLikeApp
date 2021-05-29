using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;

namespace BookingLikeApp.Areas.Admin.ViewModels
{
	public class SearchUsersViewModel
	{
		public PageViewModel PageViewModel { get; set; }
		public FilterUsersViewModel FilterUserViewModel { get; set; }
		public SortUsersViewModel SortUsersViewModel { get; set; }
		public List<User> Users { get; set; }
	}
}
