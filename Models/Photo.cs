using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string PhotoUrl { get; set; }

        public Apartment Apartment { get; set; }
    }
}
