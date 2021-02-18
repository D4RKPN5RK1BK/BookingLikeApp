using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Street
    {
        public int Id { get; set; }

		public int CityId { get; set; }
		[Required]
		[NotMapped]
		[DisplayName("Город")]
		public string CityName { get; set; }

        [DisplayName("Имя")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }

        public City City { get; set; }
        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
