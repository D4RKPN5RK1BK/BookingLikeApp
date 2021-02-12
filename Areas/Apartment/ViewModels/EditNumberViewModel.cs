using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class EditNumberViewModel : Registration
    {
        [Range(0, 10)]
        [DisplayName("Количество кроватей")]
        public int BedsCount { get; set; }

        [DisplayName("Название номера")]
        public string Name { get; set; }

        public NumberType NumberType { get; set; }

        public Dictionary<string, int> RoomsCount { get; set; }

        public SelectList BedsSelect { get; set; }
        public SelectList RoomsSelect { get; set; }

        public List<NumberRoom> NumberRooms { get; set; }

        public List<NumberBed> NumberBeds { get; set; }

        public EditNumberViewModel() { }

        public EditNumberViewModel(Number number)
        {
            Name = number.Name;
            NumberType = number.NumberType;
            NumberBeds = number.NumberBeds;
        } 
    }
}
