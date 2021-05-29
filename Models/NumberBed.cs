using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberBed
    {
        public int Id { get; set; }
		
		[DisplayName("Номер")]
        public int NumberId { get; set; }

		[DisplayName("Кровать")]
        public int BedId { get; set; }

		[DisplayName("Количество")]
        public int Quantity { get; set; }

        public Number Number { get; set; }
        public Bed Bed { get; set; }
    }
}
