using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberRoom
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int NumberId { get; set; }

        public Room Room { get; set; }
        public Number Number { get; set; }
        public IEnumerable<NumberRoomBed> NumberRoomBeds { get; set; }
    }
}
