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
        //http://traveleu.ru/accommodation/hotelRooms.htm
        public int Id { get; set; }

        [MaxLength(4, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Код")]
        public string ShortName { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Наименование")]
        public string LongName { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
