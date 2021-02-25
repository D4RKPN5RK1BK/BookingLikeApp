 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public bool Use { get; set; }

        public IEnumerable<ApartmentCard> ApartmentCards { get; set; }
    }
}
