using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class ApartmentType
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        
        public string PhotoUrl { get; set; }
        
        [Column(TypeName = "text")]
        public string Description { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
