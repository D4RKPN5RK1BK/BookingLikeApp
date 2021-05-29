using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class User : IdentityUser
    {
        [MaxLength(256)]
		protected string _displayName { get; set; }
        [DisplayName("Отображаемое имя")]
        public string DisplayName 
		{ 
			get
			{
				if (string.IsNullOrEmpty(_displayName))
					return UserName;
				else
					return _displayName;
			}
			set => _displayName = value;
		}

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
		public DateTime? LastPasswordUpdate { get; set; }

		public decimal Wallet { get; set; }

        public List<Apartment> Apartments { get; set; }
        public List<Reservation> Reservations { get; set; }

		[InverseProperty("InputUser")]
		public List<Transaction> InputTransactions { get; set; }
		[InverseProperty("OutputUser")]
		public List<Transaction> OutputTransactions { get; set; }
		[NotMapped]
		public List<Transaction> Transactions { get; set; }
	}
}
