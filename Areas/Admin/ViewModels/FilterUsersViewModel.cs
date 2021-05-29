using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;

namespace BookingLikeApp.Areas.Admin.ViewModels
{
	public class FilterUsersViewModel
	{
		public FilterUsersViewModel(string name, DateTime? registrationDateBegin, DateTime? registrationDateEnd, bool withApartments)
		{
			Name = name;
			RegistrationDateBegin = registrationDateBegin;
			RegistrationDateEnd = registrationDateEnd;
			WithApartments = withApartments;
		}

		public string Name { get; set; }
		[DataType(DataType.Date)]
		public DateTime? RegistrationDateBegin { get; set; }
		[DataType(DataType.Date)]
		public DateTime? RegistrationDateEnd { get; set; }
		public bool WithApartments { get; set; }
	}
}
