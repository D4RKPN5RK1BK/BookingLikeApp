using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Models
{
	public class PackTenant
	{
		public int Id { get; set; }
		public int PackId { get; set; }

		[DisplayName("Взрослые")]
		[Range(0, 100, ErrorMessage = "Значение поля {0} не иожет быть меньше {1} или больше {2}")]
		public int Adults { get; set; }

		[DisplayName("Дети")]
		[Range(0, 100, ErrorMessage = "Значение поля {0} не иожет быть меньше {1} или больше {2}")]
		public int Childrens { get; set; }

		[DisplayName("Стоимость")]
		public decimal Price { get; set; }

		[DisplayName("Актуальная запись")]
		public bool Current { get; set; }

		[DisplayName("Создан")]
		public DateTime TimeStamp { get; set; }

		public Pack Pack { get; set; }
	}
	
}
