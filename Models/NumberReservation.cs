using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberReservation
    {
        public int Id { get; set; }
        public int NumberId { get; set; }
        public int ReservationId { get; set; }

        [DisplayName("Reservation date")]
        public DateTimeOffset ReservationDate { get; set; }

        public Reservation Reservation { get; set; }
        public Number Number { get; set; }
    }
}
