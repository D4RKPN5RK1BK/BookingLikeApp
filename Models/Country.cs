using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(2)]
        public string Code { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        
        /*[MaxLength(256)]
        public string NormalizedName { get; set; }*/
        
        public string PhotoUrl { get; set; }
        
        public string FlagUrl { get; set; }
        
        public bool Blocked { get; set; }

        public IEnumerable<City> Cities { get; set; }
    }
}
