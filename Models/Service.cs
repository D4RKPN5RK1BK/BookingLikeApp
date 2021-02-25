using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingLikeApp.Models
{
	public class Service
	{
		public int Id { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		[DisplayName("Иконка")]
		public string IconUrl { get; set; }

		[NotMapped]
		[DisplayName("Иконка")]
		public IFormFile File { get; set; }

		[NotMapped]
		public bool Select { get; set; }

		public IEnumerable<ApartmentService> ApartmentServices { get; set; }
	}
}
