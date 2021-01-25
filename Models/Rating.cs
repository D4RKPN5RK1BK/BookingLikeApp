using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public DateTime RatingDate { get; set; }

        public Apartment Apartment { get; set; }
        public User User { get; set; }
    }
}
