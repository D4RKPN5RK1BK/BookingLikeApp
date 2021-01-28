using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class ApartmentType
    {
        public int Id { get; set; }

        [DisplayName("Наименование")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }
        
        [DisplayName("Фотография")]
        public string PhotoUrl { get; set; }
        
        [DisplayName("Описание")]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
