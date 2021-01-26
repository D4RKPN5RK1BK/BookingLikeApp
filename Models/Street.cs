using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Street
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public City City { get; set; }
        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
