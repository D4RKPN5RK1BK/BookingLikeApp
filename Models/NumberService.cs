using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class NumberService
	{
		public int Id { get; set; }
		public int NumberId { get; set; }

		[Required]
		[DisplayName("Название")]
		[MaxLength(64, ErrorMessage = "Название {0} не должно превышать {1}")]
		public string Name { get; set; }

		[DisplayName("Платный")]
		public bool HavePrice { get; set; }

		[DisplayName("Стоимость")]
		public decimal? Price { get; set; }

		public Number Number { get; set; }
		public IEnumerable<PackService> PackServices { get; set; }
	}
}
