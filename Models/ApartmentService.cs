using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class ApartmentService
	{
		public int Id { get; set; }
		public int ApartmentId { get; set; }

		[Required]
		[DisplayName("Название сервиса")]
		public string Name { get; set; }

		[DisplayName("Имеет стоимость")]
		public bool HavePrice { get; set; }

		[DisplayName("Стоимость")]
		public decimal? Price { get; set; }

		public Apartment Apartment { get; set; }
	}
}
