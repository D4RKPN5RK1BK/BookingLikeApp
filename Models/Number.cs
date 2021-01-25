using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Models
{
    public class Number
    {
        //Сделать BadTypes, bath, количество мест
        //резервация кроватей а не номеров
        //прикрутить оплату к резервациям
        //прикрутить рейтинг к резервации

        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int NumberTypeId { get; set; }
       
        [Range(0, 100)]
        [Column(TypeName = "decimal(3,0)")]
        public decimal Badrooms { get; set; }
        
        [Range(0, 100)]
        [Column(TypeName = "decimal(3,0)")]
        public decimal Kitchens { get; set; }
        
        [Range(0, 100)]
        [Column(TypeName = "decimal(3,0)")]
        public decimal Bathrooms { get; set; }
        
        [Range(0, 1000)]
        [Column(TypeName = "decimal(4,0)")]
        public decimal Area { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [Range(minimum:0, maximum:1000000000)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        public bool Enabled { get; set; }
        public bool MiniBar { get; set; }
        public bool ProvidedTV { get; set; }
        public bool FreeWiFi { get; set; }
        public bool SoundIsolation { get; set; }
        public bool OceanView { get; set; }
        public bool CityView { get; set; }
        public bool AllowSmoke { get; set; }

        public DateTimeOffset Disabled { get; set; }

        public Apartment Apartment { get; set; }
        public NumberType NumberType { get; set; } 
        public IEnumerable<NumberBed> NumberBeds { get; set; }
    }
}
