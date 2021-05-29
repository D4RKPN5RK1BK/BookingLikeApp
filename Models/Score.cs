using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class Score
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public int MaxValue { get; set; }
		public List<ReviewScore> ReviewScores { get; set; }
		
		[NotMapped]
		public float? AvgValue { get; set; }
	}
}
