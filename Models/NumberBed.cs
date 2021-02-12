using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberBed
    {
        public int Id { get; set; }
        public int NumberId { get; set; }
        public int BedId { get; set; }

        public int Quantity { get; set; }

        public Number Number { get; set; }
        public Bed Bed { get; set; }
    }
}
