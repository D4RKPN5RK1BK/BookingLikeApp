using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class EntityReservationDataPack
	{
		public Models.EntityReservation EntityReservation { get; set; }
		public Models.Number Number { get; set; }
		public Models.Pack Pack { get; set; }
		public Models.PackTenant PackTenant { get; set; }
		public int Count { get; set; }
	}

	public class ReservationReadViewModel
	{
		[DisplayName("Возможно оформить")]
		public bool AllowAccept { get; set; }
		public Models.Reservation Reservation { get; set; }
		public List<Models.Score> Scores { get; set; }
 		public List<EntityReservationDataPack> ERDataPacks { get; set; }
	}
}
