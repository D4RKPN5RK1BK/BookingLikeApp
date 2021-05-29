using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberEntity
    {
        public int Id { get; set; }
        public int NumberId { get; set; }

		[MaxLength(32, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
		[DisplayName("Название номера")]
		public string Name { get; set; }

		public bool Enable { get; set; }

        public List<EntityReservation> EntityReservations { get; set; }
        public Number Number { get; set; }

		[NotMapped]
		public List<Reservation> Reservations { get; set; }

		public bool IsFree(DateTime begin, DateTime end, List<Reservation> reservations) =>
			Enable && reservations.All(o => o.ReservationBegin > begin && o.ReservationEnd > begin || o.ReservationBegin < begin && o.ReservationEnd < end);

	}
}
