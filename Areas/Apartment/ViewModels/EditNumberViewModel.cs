using BookingLikeApp.Models;
using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class EditNumberViewModel : Registration
    {
		public  Number Number { get; set; }

		[DisplayName("Комнаты")]
		public List<Room> Rooms { get; set; }

		[DisplayName("Комнаты")]
		public SelectList BedsSelect { get; set; }

		[DisplayName("Количество номеров")]
		[Range(0, 1000, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
		public int Count { get; set; }
	}
}
