using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BookingLikeApp.Models
{
    public class City
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        
        /*[MaxLength(256)]
        public string NormalizedName { get; set; }*/

        public string PhotoUrl { get; set; }
        
        public bool Blocked { get; set; }

        public Country Country { get; set; }
        public IEnumerable<Street> Streets { get; set; }
    }
}
