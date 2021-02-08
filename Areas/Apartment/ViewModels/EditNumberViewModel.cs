using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class EditNumberViewModel : Registration
    {
        public List<NumberBed> NumberBeds { get; set; }
        public List<NumberRoom> NumberRooms { get; set; }
        public List<NumberRoomBed> NumberRoomBeds { get; set; }

        public SelectList Rooms { get; set; }
        public SelectList Beds { get; set; }

        public NumberType NumberType { get; set; }
        public Number Number { get; set; }
        
        public bool NotFound { get; set; }

        public EditNumberViewModel() { }

        public EditNumberViewModel(Models.Apartment apartment) : base(apartment)
        {
            Number = apartment.Number;
            NumberType = apartment.Number.NumberType;
        } 
    }
}
