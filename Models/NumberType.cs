using System;
using System.Collections.Generic;
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

        [MaxLength(4)]
        public string ShortName { get; set; }

        [MaxLength(256)]
        public string LongName { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
