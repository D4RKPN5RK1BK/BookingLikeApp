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

        [DisplayName("Код")]
        [MaxLength(2, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Code { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [DisplayName("Фотография")]
        public string PhotoUrl { get; set; }
		[NotMapped]
		public IFormFile PhotoFile { get; set; }
        
        [DisplayName("Флаг")]
        public string FlagUrl { get; set; }
		[NotMapped]
		public IFormFile FlagFile { get; set; }

		[DisplayName("Заблокирована")]
        public bool Blocked { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
