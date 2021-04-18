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
		public int PackTenantId { get; set; }

		public Reservation Reservation { get; set; }
		public NumberEntity NumberEntity { get; set; }
		public PackTenant PackTenant { get; set; }
	}
}
