using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberType
    {
        public int Id { get; set; }

        [DisplayName("Наименование")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }

        [DisplayName("Кодовое название")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
		public string Code { get; set; }


        [DisplayName("Имеет комнаты")]
        public bool HasRooms { get; set; }
        [DisplayName("Общий номер")]
        public bool Share { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public IEnumerable<Number> Numbers { get; set; }
    }
}
