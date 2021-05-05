using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class ReviewScore
	{
		public int Id { get; set; }

		public int ReviewId { get; set; }
		public int ScoreId { get; set; }

		public int Value { get; set; }

		public Review Review { get; set; }
		public Score Score { get; set; }
	}
}
