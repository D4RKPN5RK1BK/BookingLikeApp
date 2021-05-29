using BookingLikeApp.Areas.Apartment.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingLikeApp.Models
{
    public class Number
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        [DisplayName("Тип номера")]
        public int NumberTypeId { get; set; }
        
        [DisplayName("Площадь (м^2)")]
        [Range(0, 200, ErrorMessage = "Значение для {0} должно должно быть от {1} до {2}")]
        public int Area { get; set; }

        [DisplayName("Отображаемое имя")]
        [MaxLength(256, ErrorMessage = "Длинна строки {0} не может превышать {1}")]
        public string Name { get; set; }

        [DisplayName("Закончен")]
		public bool Finish { get; set; }

        [DisplayName("Доступен")]
        public bool Enable { get; set; }

		public Apartment Apartment { get; set; }
        public NumberType NumberType { get; set; }
        public List<NumberBed> NumberBeds { get; set; }
        public List<NumberEntity> NumberEntities { get; set; }
        public List<NumberService> NumberServices { get; set; }
        public List<Pack> Packs { get; set; }

		[NotMapped]
		public int Count { get; set; }

		public bool HaveFreeEntities(DateTime begin, DateTime end, List<Reservation> reservations) =>
			Enable && NumberEntities
				.Any(o => o
					.IsFree(begin, end, reservations
						.Where(r => r.EntityReservations
						.Any(er => er.NumberEntityId == o.Id) && !r.Cencel).ToList()));

		public NumberEntity GetFreeEntity(DateTime begin, DateTime end, List<Reservation> reservations) =>
			NumberEntities
				.FirstOrDefault(o => o
					.IsFree(begin, end, reservations
						.Where(r => r.EntityReservations
							.Any(er => er.NumberEntityId == o.Id) && !r.Cencel).ToList()));

		public void SortFreeEntities(DateTime begin, DateTime end, List<Reservation> reservations) =>
			NumberEntities = NumberEntities
				.Where(o => o
					.IsFree(begin, end, reservations
						.Where(r => r.EntityReservations
							.Any(er => er.NumberEntityId == o.Id) && !r.Cencel).ToList())).ToList();

		public List<NumberEntity> GetFreeEntities(DateTime begin, DateTime end, List<Reservation> reservations) =>
			NumberEntities
				.Where(o => o
					.IsFree(begin, end, reservations
						.Where(r => r.EntityReservations
							.Any(er => er.NumberEntityId == o.Id) && !r.Cencel).ToList())).ToList();
	}
}
