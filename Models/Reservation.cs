using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BedId { get; set; }
        public DateTimeOffset ReservationDate { get; set; }
        public DateTime ProceedDateTime { get; set; }
        public DateTime AbortCancel { get; set; }

        public User User { get; set; }
        public Bed Bed { get; set; }
    }
}
