using BookingLikeApp.Areas.Apartment.ViewModels;
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
        
        //Основная информация
        [DisplayName("Улица")]
        public int ApartmentStreetId { get; set; }
        
        [DisplayName("Тип жилья")]
        public int ApartmentTypeId { get; set; }

        [Range(1, 5, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}" )]
        [DisplayName("Количество звезд")]
        public int Stars { get; set; }

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
        public string ContactPhone { get; set; }

        [MaxLength(20, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Дополнительный номер телефона")]
        public string AdditionalPhone { get; set; }

        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Дополнительная адресная строка")]
        public string SecondAddressLine { get; set; }
        
        [DisplayName("Логотип")]
        public string LogoUrl { get; set; }
        
        public DateTimeOffset Disabled { get; set; }

        //Услуги
        [DisplayName("Парковка")]
        public int Parking { get; set; }

        [DisplayName("Завтрак")]
        public int Breakfest { get; set; }

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

        //Порядок проживания
        [DisplayName("Разрешены дети")]
        public bool ChildrensAllowed { get; set; }
        [DisplayName("Разрешены животные")]
        public bool AnimalsAllowed { get; set; }

        [Range(0, 14, ErrorMessage = "Значение для {0} должно быть от {1} до {2}")]
        [DisplayName("Количество дней до начала резерванции")]
        public int DaysUntilCancelEnds { get; set; }
        [DisplayName("Стоимость отмены бронирования")]
        public bool CancelPrice { get; set; }

        [DisplayName("Стоимость отмены бронирования")]
        public bool AccidentProtection { get; set; }

        [DataType(DataType.Time)]
        [DisplayName("Начало регистрации приезда")]
        public DateTime ArrivalTimeStarts { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Конец регистрации приезда")]
        public DateTime ArrivalTimeEnds { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Начало регистрации отъезда")]
        public DateTime DepartureTimeStarts { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Конец регистрации отъезда")]
        public DateTime DepartureTimeEnds { get; set; }

        //Оплата проживания
        [DisplayName("Списывать деньги при помощи карт")]
        public bool UseCards { get; set; }
        [DisplayName("Проверено")]
        public bool Checked { get; set; }
        public bool Enable { get; set; }
        public bool Finished { get; set; }

        [DisplayName("Место проживаниия платильщика")]
        public int PolisherStreetId { get; set; }

        public User User { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public Registration Registration { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public IEnumerable<Number> Numbers { get; set; }
        public IEnumerable<ApartmentCard> ApartmentCards { get; set; }

        [NotMapped]
        public Number Number { get; set; }

        public void SetBasicInfo(BasicInfoViewModel model)
        {
            ApartmentStreetId = model.ApartmentStreetId;
            Stars = model.Stars;
            Name = model.Name;
            Description = model.Description;
            ContactPerson = model.ContactPerson;
            ContactPhone = model.ContactPhone;
            AdditionalPhone = model.AdditionalPhone;
            SecondAddressLine = model.SecondAddressLine;
        }

        public void SetServices(ServicesViewModel model)
        {
            Parking = model.Parking;
            Breakfest = model.Breakfest;
            Bar = model.Bar;
            FreeWiFi = model.FreeWiFi;
            Fitnes = model.Fitnes;
            Pool = model.Pool;
            FullTimeRegistration = model.FullTimeRegistration;
            FamilyNumbers = model.FamilyNumbers;
            SmokeFreeNumbers = model.SmokeFreeNumbers;
        }

        public void  SetRules(RulesViewModel model)
        {
            ChildrensAllowed = model.ChildrensAllowed;
            AnimalsAllowed = model.AnimalsAllowed;
            DaysUntilCancelEnds = model.DaysUntilCancelEnds;
            CancelPrice = model.CancelPrice;
            AccidentProtection = model.AccidentProtection;
            ArrivalTimeStarts = model.ArrivalTimeStarts;
            ArrivalTimeEnds = model.ArrivalTimeEnds;
            DepartureTimeStarts = model.DepartureTimeStarts;
            DepartureTimeEnds = model.DepartureTimeEnds;
        }

        public void SetPayment(PaymentViewModel model)
        {

        }
    }
}
