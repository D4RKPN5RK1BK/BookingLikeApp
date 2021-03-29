using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		

		[DisplayName("Подтвержден")]
		public bool Confirm { get; set; }
		[DisplayName("Отменен")]
		public bool Cencel { get; set; }

		

		[DisplayName("Оценка")]
		[Range(1, 10, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
		[Column(TypeName = "decimal(2,0)")]
		public decimal Points { get; set; }

		[DisplayName("Начало бронирования")]
		public DateTime ReservationBegin { get; set; }

		[DisplayName("Конец бронирования")]
		public DateTime ReservationEnd { get; set; }

		[DisplayName("Стоимость")]
		[Column(TypeName = "decimal(12,2)")]
		public decimal Price { get; set; }

		[DisplayName("Дата оформления заявки")]
		public DateTime TimeStamp { get; set; }

		[DisplayName("Крайний срок отмены")]
		public DateTime AbortCancel { get; set; }

		public User User { get; set; }
		
		public List<EntityReservation> EntityReservations { get; set; }
	}
}
