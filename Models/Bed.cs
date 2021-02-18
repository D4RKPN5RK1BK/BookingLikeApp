using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Bed
    {
        public int Id { get; set; }

        //public int RoomId { get; set; }

        [MaxLength(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [MaxLength(256)]
		[DisplayName("Кодовое имя")]
		public string Code { get; set; }

        [DisplayName("Вместимость")]
        [Column(TypeName = "decimal(1,0)")]
        public decimal Capacity { get; set; }

        //public Room Room { get; set; }
        public IEnumerable<NumberBed> NumberBeds { get; set; }
        public IEnumerable<NumberRoomBed> NumberRoomBeds { get; set; }

    }
}
