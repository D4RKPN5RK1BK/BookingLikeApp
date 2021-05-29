using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.ViewModels
{
	public class ProfileImageViewModel
	{
		public string Path { get; set; }
		public int Size { get; set; }

		public ProfileImageViewModel(string path = "", int size = 20)
		{
			Path = path;
			Size = size;
		}
	}
}
