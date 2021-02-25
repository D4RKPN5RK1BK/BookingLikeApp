using BookingLikeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class FacilitesViewModel : Registration
    {
		public int Id { get; set; }
        public FacilitesViewModel() { }

        public FacilitesViewModel(Models.Apartment apartment) : base(apartment)
        {
			Id = apartment.Id;
		}
    }
}
