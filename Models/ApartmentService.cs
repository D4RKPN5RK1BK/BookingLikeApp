using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class ApartmentService
	{
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public int ApartmentId { get; set; }

		public Apartment Apartment { get; set; }
		public Service Service { get; set; }
	}
}
