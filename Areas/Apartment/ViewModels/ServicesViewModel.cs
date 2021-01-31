using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class ServicesViewModel
    {
        //Парковка
        [DisplayName("Парковка")]
        [Required(ErrorMessage = "Данное поле необходимо здя заполнения")]
        [Range(0, 2, ErrorMessage = "Данное значение должно быть от {1} до {2}")]
        public int Parking { get; set; }

        //Завтрак
        [DisplayName("Завтрак")]
        [Required(ErrorMessage = "Данное поле необходимо здя заполнения")]
        [Range(0, 2, ErrorMessage = "Данное значение должно быть от {1} до {2}")]
        public int Breakfest { get; set; }

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

        public ServicesViewModel() { }

        public ServicesViewModel(Models.Apartment apartment)
        {
            Parking = apartment.Parking;
            Breakfest = apartment.Breakfest;
            Bar = apartment.Bar;
            FreeWiFi = apartment.FreeWiFi;
            Fitnes = apartment.Fitnes;
            Pool = apartment.Pool;
            FullTimeRegistration = apartment.FullTimeRegistration;
            FamilyNumbers = apartment.FamilyNumbers;
            SmokeFreeNumbers = apartment.SmokeFreeNumbers;
        }
    }
}
