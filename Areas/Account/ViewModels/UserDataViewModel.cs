using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingLikeApp.Areas.Account.ViewModels
{
	public class UserDataViewModel
	{
		public string DisplayName { get; set; }
		public DateTime BirthDate { get; set; }
		public bool Gender { get; set; }

		public bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(DisplayName))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

	}
}
