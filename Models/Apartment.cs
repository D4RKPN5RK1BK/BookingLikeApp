using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        
        [DisplayName("Address line")]
        public int StreetId { get; set; }
        
        [DisplayName("Type name")]
        public int ApartmentTypeId { get; set; }
        
        [Range(1, 5)]
        [Column(TypeName ="decimal(1,0)")]
        public decimal Stars { get; set; }

        [DisplayName("Days until cancel ends")]
        [Column(TypeName ="decimal(2,0)")]
        public decimal DaysUntilCancelEnds { get; set; }

        [DisplayName("User")]
        public string UserId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        
        [Column(TypeName = "text")]
        public string Description { get; set; }
        
        [MaxLength(256)]
        [DisplayName("Contact person")]
        public string ContactPerson { get; set; }

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Contact number")]
        public string ContactNumber { get; set; }

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Additional number")]
        public string AdditionalNumber { get; set; }

        [MaxLength(256)]
        [DisplayName("Second addressLine")]
        public string SecondAddressLine { get; set; }
        
        public string LogoUrl { get; set; }

        [DisplayName("Full price cencel")]
        public bool FullPriceCencel { get; set; }
        
        //Управление
        public bool Checked { get; set; }
        public bool Enabled { get; set; }

        //Парковка
        public bool Parking { get; set; }
        public bool FreeParking { get; set; }

        //Завтрак
        public bool Breakfest { get; set; }
        public bool BreakfestIncluded { get; set; }

        //???
        [DisplayName("Childrens allowed")]
        public bool ChildrensAllowed { get; set; }
        [DisplayName("Animals allowed")]
        public bool AnimalsAllowed { get; set; }

        //Удобства
        public bool Bar { get; set; }
        public bool FreeWiFi { get; set; }
        public bool Fitnes { get; set; }
        public bool Pool { get; set; }
        [DisplayName("Full time registration")]
        public bool FullTimeRegistration { get; set; }
        [DisplayName("Family numbers")]
        public bool FamilyNumbers { get; set; }
        [DisplayName("Smoke free numbers")]
        public bool SmokeFreeNumbers { get; set; }

        [DisplayName("Foundation date")]
        public DateTime? FoundationDate { get; set; }

        public DateTimeOffset Disabled { get; set; }

        public User User { get; set; }
        public Street Street { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}
