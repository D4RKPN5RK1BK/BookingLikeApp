using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class Transaction
	{
		[Key]
		public int Id { get; set; }
		public string InputUserId { get; set; }
		public string OutputUserId { get; set; }
		public int? ReservationId { get; set; }
		public decimal Value { get; set; }
		public DateTime TimeStamp { get; set; }

		public Reservation Reservation { get; set; }
		public User InputUser { get; set; }
		public User OutputUser { get; set; }
	}
}
