using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Room
    {
        public int Id { get; set; }

        [DisplayName("Название комнаты")]
        public string Name { get; set; }

		[NotMapped]
		[DisplayName("Количество")]
		public int Count { get; set; }
        
        public IEnumerable<NumberRoom> NumberRooms { get; set; } 
        public IEnumerable<Bed> Beds { get; set; }
    }
}
