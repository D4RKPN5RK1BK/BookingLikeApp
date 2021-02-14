using BookingLikeApp.Models;
using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class EditNumberViewModel : Registration
    {
        [Range(0, 10)]
        [DisplayName("Количество кроватей")]
        public int BedsCount { get; set; }
        public Dictionary<string, int> RoomsCount { get; set; }

        public SelectList BedsSelect { get; set; }
        public SelectList RoomsSelect { get; set; }

        [DisplayName("Площадь (м^2)")]
        [Range(0, 200, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Area { get; set; }
        [DisplayName("Отображаемое имя")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }
        [DisplayName("Стоимость (за ночь)")]
        [Range(minimum: 0, maximum: 1000000000, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [DisplayName("Доступен")]
        public bool Enable { get; set; }
        [DisplayName("Мини бар")]
        public bool MiniBar { get; set; }
        [DisplayName("Присутствует телевидение")]
        public bool ProvidedTV { get; set; }
        [DisplayName("Разрешается курить в номере")]
        public bool AllowSmoke { get; set; }

        public NumberType NumberType { get; set; }
        public List<NumberBed> NumberBeds { get; set; }
        public List<NumberRoom> NumberRooms { get; set; }

        public EditNumberViewModel() { }

        public EditNumberViewModel(Number number)
        {
            Area = number.Area;
            Name = number.Name;
            Price = number.Price;
            Enable = number.Enable;
            MiniBar = number.MiniBar;
            ProvidedTV = number.ProvidedTV;
            AllowSmoke = number.AllowSmoke;

            if (NumberType != null) NumberType = number.NumberType;
            if (NumberBeds != null) NumberBeds = number.NumberBeds;
            if (NumberRooms != null) NumberRooms = number.NumberRooms;
        } 
    }
}
