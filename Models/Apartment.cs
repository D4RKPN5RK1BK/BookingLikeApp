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
        
        [DisplayName("Улица")]
        public int StreetId { get; set; }
        
        [DisplayName("Тип жилья")]
        public int ApartmentTypeId { get; set; }

        [Range(1, 5, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}" )]
        [DisplayName("Количество звезд")]
        public int Stars { get; set; }

        [Range(1, 14, ErrorMessage = "Значение для {0} должно быть от {1} до {2}")]
        [DisplayName("Количество дней до начала резерванции")]
        public int DaysUntilCancelEnds { get; set; }

        [DisplayName("Владелец")]
        public string UserId { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Наименование собственности")]
        public string Name { get; set; }
        
        [Column(TypeName = "text")]
        [DisplayName("Описание")]
        public string Description { get; set; }
        
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Имя человека для обратной связи")]
        public string ContactPerson { get; set; }

        [MaxLength(20, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Контактный номер телефона")]
        public string ContactNumber { get; set; }

        [MaxLength(20, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Дополнительный номер телефона")]
        public string AdditionalNumber { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Дополнительная адресная строка")]
        public string SecondAddressLine { get; set; }
        
        [DisplayName("Логотип")]
        public string LogoUrl { get; set; }

        [DisplayName("Стоимость отмены бронирования")]
        public bool CancelPrice { get; set; }
        
        //Нужно лишь для того чтобы можно было бросить регистрацию на пол пути
        public bool Finished { get; set; }
        
        //Управление
        [DisplayName("Проверено")]
        public bool Checked { get; set; }
        [DisplayName("Доступно")]
        public bool Enabled { get; set; }

        //Парковка
        [DisplayName("Парковка")]
        public int Parking { get; set; }

        //Завтрак
        [DisplayName("Завтрак")]
        public int Breakfest { get; set; }

        //???
        [DisplayName("Разрешены дети")]
        public bool ChildrensAllowed { get; set; }
        [DisplayName("Разрешены животные")]
        public bool AnimalsAllowed { get; set; }

        //Удобства
        [DisplayName("Бар")]
        public bool Bar { get; set; }
        [DisplayName("Бесплатный вай фай")]
        public bool FreeWiFi { get; set; }
        [DisplayName("Фитнес центр")]
        public bool Fitnes { get; set; }
        [DisplayName("Бассейн")]
        public bool Pool { get; set; }
        [DisplayName("Регитрация полные сутки")]
        public bool FullTimeRegistration { get; set; }
        [DisplayName("Семеные номера")]
        public bool FamilyNumbers { get; set; }
        [DisplayName("Номера для некурящих")]
        public bool SmokeFreeNumbers { get; set; }

        public DateTimeOffset Disabled { get; set; }

        public User User { get; set; }
        public Street Street { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Number> Numbers { get; set; }
    }
}
