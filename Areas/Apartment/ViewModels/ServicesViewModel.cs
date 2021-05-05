using BookingLikeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class ServicesViewModel : Registration
    {
		public int Id { get; set; }

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

		public List<ApartmentService> ApartmentServices { get; set; }

        public ServicesViewModel() { }

        public ServicesViewModel(Models.Apartment apartment) : base(apartment)
        {
			Id = apartment.Id;
            Parking = apartment.Parking;
            Breakfest = apartment.Breakfest;
        }
    }
}
