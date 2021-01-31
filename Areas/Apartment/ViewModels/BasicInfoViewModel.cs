using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class BasicInfoViewModel
    {
        [DisplayName("Улица")]
        [Required(ErrorMessage = "Данный пункт обязателен")]
        public int ApartmentStreetId { get; set; }

        [Required(ErrorMessage = "Данный пункт обязателен")]
        [Range(0, 5, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        [DisplayName("Количество звезд")]
        public int Stars { get; set; }

        [Required(ErrorMessage = "Данный пункт обязателен")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Наименование собственности")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Для дальнейшей работы нам можут потребоваться Имя ответственного за данную недвижемость")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Имя человека для обратной связи")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Данный пункт обязателен")]
        [MaxLength(20, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Контактный номер телефона")]
        public string ContactPhone { get; set; }

        [MaxLength(20, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Дополнительный номер телефона")]
        public string AdditionalPhone { get; set; }

        [Required(ErrorMessage = "Данный пункт обязателен")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        [DisplayName("Дополнительная адресная строка")]
        public string SecondAddressLine { get; set; }

        public BasicInfoViewModel() { }

        public BasicInfoViewModel(Models.Apartment apartment)
        {
            ApartmentStreetId = apartment.ApartmentStreetId;
            Stars = apartment.Stars;
            Name = apartment.Name;
            Description = apartment.Description;
            ContactPerson = apartment.ContactPerson;
            ContactPhone = apartment.ContactPhone;
            AdditionalPhone = apartment.AdditionalPhone;
            SecondAddressLine = apartment.SecondAddressLine;
        }

    }
}
