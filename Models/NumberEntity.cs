using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class NumberEntity
    {
        public int Id { get; set; }
        public int NumberId { get; set; }

        public List<EntityReservation> EntityReservations { get; set; }
        public Number Number { get; set; }
    }
}
