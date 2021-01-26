using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class User : IdentityUser
    {
        [DisplayName("Display Name")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [DisplayName("Avatar")]
        public string AvatarUrl { get; set; }

        public bool? Gender { get; set; }

        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Registration date")]
        [DataType(DataType.Date)]
        public DateTime RegistationDate { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
