using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberRoomBed
    {
        public int Id { get; set; }
        public int NumberRoomId { get; set; }
        public int BedId { get; set; }

        public NumberRoom NumberRoom { get; set; }
        public Bed Bed { get; set; }
    }
}
