using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class RulesViewModel
    {
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

        public RulesViewModel() { }

        public RulesViewModel(Models.Apartment apartment)
        {
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
