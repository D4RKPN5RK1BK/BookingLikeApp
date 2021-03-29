using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class EntityReservation
    {
        public int Id { get; set; }
		public int ReservationId { get; set; }
		public int NumberEntityId { get; set; }
		public int? PackId { get; set; }
		public int? PackTenantId { get; set; }

		[DisplayName("Взрослые")]
		[Range(1, int.MaxValue, ErrorMessage = "Значение не может быть отрицательным или превышать {2}")]
		public int Adults { get; set; }
		[DisplayName("Дети")]
		[Range(1, int.MaxValue, ErrorMessage = "Значение не может быть отрицательным или превышать {2}")]
		public int Childrens { get; set; }

		public Reservation Reservation { get; set; }
		public NumberEntity NumberEntity { get; set; }
		public Pack Pack { get; set; }
		public PackTenant PackTenant { get; set; }
	}
}
