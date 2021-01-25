using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public int StreetId { get; set; }
        public int ApartmentTypeId { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(3,0)")]
        public decimal Pools { get; set; }
        
        [Range(1, 5)]
        [Column(TypeName ="decimal(1,0)")]
        public decimal Stars { get; set; }
        public string UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public string AddressLine { get; set; }
        public string LogoUrl { get; set; }

        public bool Checked { get; set; }
        public bool Enabled { get; set; }
        public bool FreeWiFi { get; set; }
        public bool FreeParking { get; set; }
        public bool ChildrenAllowed { get; set; }
        public bool AnimalsAllowed { get; set; }
        public bool Bar { get; set; }

        public DateTime? FoundationDate { get; set; }

        public DateTimeOffset Disabled { get; set; }

        public User User { get; set; }
        public Street Street { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
