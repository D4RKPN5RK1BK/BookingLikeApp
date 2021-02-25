using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class RulesViewModel : Registration
    {
		public int Id { get; set; }

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
        [CompareWithStart("ArrivalTimeStarts", ErrorMessage = "Значения конца приема должно быть больше чем начальное значение.")]
        public DateTime ArrivalTimeEnds { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Начало регистрации отъезда")]
        public DateTime DepartureTimeStarts { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Конец регистрации отъезда")]
        [CompareWithStart("DepartureTimeStarts", ErrorMessage = "Значения конца приема должно быть больше чем начальное значение.")]
        public DateTime DepartureTimeEnds { get; set; }

        public SelectList TimePoints = new SelectList(
            new Dictionary<string, DateTime>()
            {
                {"7:00", new DateTime(1, 1, 1, 7, 0, 0)},
                {"7:30", new DateTime(1, 1, 1, 7, 30, 0)},
                {"8:00", new DateTime(1, 1, 1, 8, 0, 0)},
                {"8:30", new DateTime(1, 1, 1, 8, 30, 0)},
                {"9:00", new DateTime(1, 1, 1, 9, 0, 0)},
                {"9:30", new DateTime(1, 1, 1, 9, 30, 0)},
                {"10:00", new DateTime(1, 1, 1, 10, 0, 0)},
                {"10:30", new DateTime(1, 1, 1, 10, 30, 0)},
                {"11:00", new DateTime(1, 1, 1, 11, 0, 0)},
                {"11:30", new DateTime(1, 1, 1, 11, 30, 0)},
                {"12:00", new DateTime(1, 1, 1, 12, 0, 0)},
                {"12:30", new DateTime(1, 1, 1, 12, 30, 0)},
                {"13:00", new DateTime(1, 1, 1, 13, 0, 0)},
                {"13:30", new DateTime(1, 1, 1, 13, 30, 0)},
                {"14:00", new DateTime(1, 1, 1, 14, 0, 0)},
                {"14:30", new DateTime(1, 1, 1, 14, 30, 0)},
                {"15:00", new DateTime(1, 1, 1, 15, 0, 0)},
                {"15:30", new DateTime(1, 1, 1, 15, 30, 0)},
                {"16:00", new DateTime(1, 1, 1, 16, 0, 0)},
                {"16:30", new DateTime(1, 1, 1, 16, 30, 0)},
                {"17:00", new DateTime(1, 1, 1, 17, 0, 0)},
                {"17:30", new DateTime(1, 1, 1, 17, 30, 0)},
                {"18:00", new DateTime(1, 1, 1, 18, 0, 0)},
                {"18:30", new DateTime(1, 1, 1, 18, 30, 0)},
                {"19:00", new DateTime(1, 1, 1, 19, 0, 0)},
                {"19:30", new DateTime(1, 1, 1, 19, 30, 0)},
                {"20:00", new DateTime(1, 1, 1, 20, 0, 0)},
            },
            "Value",
            "Key"
        );

        public RulesViewModel() { }

        public RulesViewModel(Models.Apartment apartment) : base(apartment)
        {
			Id = apartment.Id;
			ChildrensAllowed = apartment.ChildrensAllowed;
            AnimalsAllowed = apartment.AnimalsAllowed;
            DaysUntilCancelEnds = apartment.DaysUntilCancelEnds;
            CancelPrice = apartment.CancelPrice;
            AccidentProtection = apartment.AccidentProtection;
            ArrivalTimeStarts = apartment.ArrivalTimeStarts;
            ArrivalTimeEnds = apartment.ArrivalTimeEnds;
            DepartureTimeStarts = apartment.DepartureTimeStarts;
            DepartureTimeEnds = apartment.DepartureTimeEnds;
        }    
    }

    public class CompareWithStart : ValidationAttribute
    {
        private readonly string _prop;

        public CompareWithStart(string prop)
        {
            _prop = prop;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            DateTime currentValue = (DateTime)value;

            var prop = validationContext.ObjectType.GetProperty(_prop);

            if (prop == null)
                throw new ArgumentException("Поле с двннным значением не найдено.");

            var comparsionValue = (DateTime)prop.GetValue(validationContext.ObjectInstance);

            if (currentValue < comparsionValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
