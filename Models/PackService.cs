using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class PackService
	{
		public int Id { get; set; }
		public int PackId { get; set; }
		public int? NumberServiceId { get; set; }

		public Pack Pack { get; set; }
		public NumberService NumberService { get; set; }
	}
}
