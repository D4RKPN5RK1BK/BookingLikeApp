using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class ApartmentCard
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int CardId { get; set; }

        public Apartment Apartment { get; set; }
        public Card Card { get; set; }
    }
}
