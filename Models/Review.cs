using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class Review
	{
		[Key]
		public int ReservationId { get; set; }

		[DisplayName("Сообщение")]
		[DataType(DataType.Text)]
		public string Message { get; set; }

		[DisplayName("Сообщение")]
		public DateTime CreateAt { get; set; }

		public Reservation Reservation { get; set; }
		public List<ReviewScore> ReviewScores { get; set; }
	}
}
