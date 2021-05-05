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
        [MaxLength(256)]
        [DisplayName("Отображаемое имя")]
        public string DisplayName { get; set; }

        [DisplayName("Изображение профиля")]
        public string PFPUrl { get; set; }

        [DisplayName("Пол")]
        public bool? Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата рождения")]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата регистрации")]
        public DateTime RegistationDate { get; set; }

        [DataType(DataType.Date)]
		[DisplayName("Последнее обновление пароля")]
		public DateTime LastPasswordUpdate { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
