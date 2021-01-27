﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Bed
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(1,0)")]
        public decimal Capacity { get; set; }

        public IEnumerable<Number> Numbers { get; set; }
    }
}