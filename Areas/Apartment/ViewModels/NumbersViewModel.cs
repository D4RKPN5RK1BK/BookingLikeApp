using BookingLikeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class NumbersViewModel : Registration
    {
        public Number Number { get; set; }

        public NumbersViewModel() { }

        public NumbersViewModel(Models.Apartment apartment) : base(apartment)
        {
            if (apartment.Numbers != null)
            {
                Number = apartment.Numbers.FirstOrDefault();
            }
        }
    }
}
