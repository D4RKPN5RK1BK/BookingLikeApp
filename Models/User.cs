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
        [DisplayName("Отображаемое имя")]
        [MaxLength(256)]
        public string DisplayName { get; set; }

        [DisplayName("Аватар")]
        public string AvatarUrl { get; set; }

        [DisplayName("Пол")]
        public bool? Gender { get; set; }

        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Дата регистрации")]
        [DataType(DataType.Date)]
        public DateTime RegistationDate { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
        public IEnumerable<EntityReservation> EntityReservations { get; set; }
    }
}
