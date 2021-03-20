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
	public class ApartmentService
	{
		public int Id { get; set; }
		public int ApartmentId { get; set; }

		[DisplayName("Название")]
		public string Name { get; set; }

		public Apartment Apartment { get; set; }
		
	}
}
