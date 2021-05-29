using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookingLikeApp.Models
{
	public class City
	{
		public int Id { get; set; }

		[Required]
		public int CountryId { get; set; }

        [DisplayName("Название")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }

        [DisplayName("Фотография")]
        public string PhotoUrl { get; set; }
		[NotMapped]
		public IFormFile File { get; set; }

		[DisplayName("Заблокирован")]
        public bool Blocked { get; set; }

        public Country Country { get; set; }
		public List<Apartment> Apartments { get; set; }
    }
}
