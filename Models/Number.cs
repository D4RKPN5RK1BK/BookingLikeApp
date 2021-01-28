using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Тип номера")]
        public int NumberTypeId { get; set; }
        [DisplayName("Тип кровати")]
        public int BedId { get; set; }
       
        [DisplayName("Количество кроватей")]
        [Range(0, 100, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int BedQuantity { get; set; }
        
        [DisplayName("Ванные комнаты")]
        [Range(0, 100, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Bathrooms { get; set; }

        [DisplayName("Спальные комнаты")]
        [Range(0, 100, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Bedrooms { get; set; }
        
        [DisplayName("Гостинные комнаты")]
        [Range(0, 100, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int LinvingRooms { get; set; }
        
        [DisplayName("Площадь (м^2)")]
        [Range(0, 200, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Area { get; set; }

        [DisplayName("Отображаемое имя")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string CustomName { get; set; }

        [DisplayName("Стоимость (за ночь)")]
        [Range(minimum:0, maximum:1000000000, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [DisplayName("Доступно")]
        public bool Enabled { get; set; }
        [DisplayName("Общее жилье")]
        public bool Shared { get; set; }
        [DisplayName("Мини бар")]
        public bool MiniBar { get; set; }
        [DisplayName("Присутствует телевидение")]
        public bool ProvidedTV { get; set; }
        [DisplayName("Бесплатный WiFi")]
        public bool FreeWiFi { get; set; }
        [DisplayName("Звукоизоляция")]
        public bool SoundIsolation { get; set; }
        [DisplayName("Вид на океан")]
        public bool OceanView { get; set; }
        [DisplayName("Вид на город")]
        public bool CityView { get; set; }
        [DisplayName("Разрешается курить в номере")]
        public bool AllowSmoke { get; set; }

        public DateTimeOffset Disabled { get; set; }

        public Apartment Apartment { get; set; }
        public NumberType NumberType { get; set; } 
        public IEnumerable<NumberReservation> NumberReservations { get; set; }
    }
}
