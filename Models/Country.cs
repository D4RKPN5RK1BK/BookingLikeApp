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
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [DisplayName("Фотография")]
        public string PhotoUrl { get; set; }
		[NotMapped]
		public IFormFile File { get; set; }

		[DisplayName("Заблокирована")]
        public bool Blocked { get; set; }

		[DisplayName("Города")]
        public List<City> Cities { get; set; }
		[DisplayName("Отели")]
		public List<Apartment> Apartments { get; set; }
	}
}
