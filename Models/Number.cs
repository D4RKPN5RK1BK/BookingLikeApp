using BookingLikeApp.Areas.Apartment.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingLikeApp.Models
{
    public class Number
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [DisplayName("Тип номера")]
        public int NumberTypeId { get; set; }
        
        [DisplayName("Площадь (м^2)")]
        [Range(0, 200, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Area { get; set; }

        [DisplayName("Отображаемое имя")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }
		
        [DisplayName("Стоимость (за ночь)")]
        [Range(minimum:0, maximum:1000000000, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [DisplayName("Доступно")]
        public bool Enable { get; set; }
        [DisplayName("Мини бар")]
        public bool MiniBar { get; set; }
        [DisplayName("Присутствует телевидение")]
        public bool ProvidedTV { get; set; }
        [DisplayName("Разрешается курить в номере")]
        public bool AllowSmoke { get; set; }

		public Apartment Apartment { get; set; }
        public NumberType NumberType { get; set; }
        public List<NumberBed> NumberBeds { get; set; }
        public List<NumberEntity> NumberEntities { get; set; }
        public List<NumberRoom> NumberRooms { get; set; }

	}
}
