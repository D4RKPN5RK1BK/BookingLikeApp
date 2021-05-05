using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingLikeApp.Areas.Account.ViewModels
{
	public class UserPFPViewModel
	{
		public IFormFile File { get; set; }
	}
}
