﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    }
}
