using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class ReservationBed
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public int NumberBedId { get; set; }

        public Reservation Reservation { get; set; }
        public NumberBed NumberBed { get; set; }
    }
}
