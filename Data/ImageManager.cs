using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingLikeApp.Data
{
	public class ImageManager
	{
		public static async Task<string> AddImageAsync(IFormFile file)
		{
			return file.FileName;
		}
	}
}
