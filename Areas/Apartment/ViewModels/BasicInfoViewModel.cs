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
        public int Id { get; set; }

        [DisplayName("Улица")]
        public int StreetId { get; set; }

        [Range(0, 5, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        [DisplayName("Количество звезд")]
        public int Stars { get; set; }

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
    }
}
