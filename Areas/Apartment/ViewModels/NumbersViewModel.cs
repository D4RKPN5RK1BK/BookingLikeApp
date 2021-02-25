using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class NumbersViewModel : Registration
    {
		public int Id { get; set; }

        public SelectList NumberTypes { get; set; }
        public List<Number> NumbersList { get; set; }

        public NumbersViewModel() { }

        public NumbersViewModel(Models.Apartment apartment) : base(apartment)
        {
			Id = apartment.Id;
		}
    }
}
