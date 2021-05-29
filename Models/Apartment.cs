using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BookingLikeApp.Attributes;

namespace BookingLikeApp.Models
{
	public class Apartment
    {
        public int Id { get; set; }
        
        //Основная информация
		[DisplayName("Город")]
		public virtual int? CityId { get; set; }
		[DisplayName("Страна")]
		public virtual int? CountryId { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayName("Дата регистрации")]
		public virtual DateTime? RegistrationDate { get; set; }
        
        [DisplayName("Тип жилья")]
        public virtual int ApartmentTypeId { get; set; }

        [Range(0, 5, ErrorMessage = "Значение для \"{0}\" должно должно быть от {1} до {2}")]
        [DisplayName("Количество звезд")]
        public virtual int Stars { get; set; }

        [DisplayName("Владелец")]
        public virtual string UserId { get; set; }
		
        [DisplayName("Наименование собственности")]
        [MaxLength(256, ErrorMessage = "Длинна строки \"{0}\" не может превышать {1}")]
        public virtual string Name { get; set; }
        
        /*[Column(TypeName = "text")]*/
        [DisplayName("Описание")]
        public virtual string Description { get; set; }

        [DisplayName("Имя человека для обратной связи")]
		[MaxLength(256, ErrorMessage = "Длинна строки \"{0}\" не может превышать {1}")]
		public virtual string ContactPerson { get; set; }

		[Phone(ErrorMessage = "Неверно указан номер телефона")]
		[MaxLength(20, ErrorMessage = "Длинна строки \"{0}\" не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        [DisplayName("Контактный номер телефона")]
        public virtual string ContactPhone { get; set; }

		[Phone(ErrorMessage = "Неверно указан номер телефона")]
		[DisplayName("Дополнительный номер телефона")]
        [MaxLength(20, ErrorMessage = "Длинна строки \"{0}\" не может превышать {1}")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Неверно указан номер телефона")]
        public virtual string AdditionalPhone { get; set; }

        [DisplayName("Дополнительная адресная строка (дом, квартира)")]
        [MaxLength(256, ErrorMessage = "Длинна строки \"{0}\" не может превышать {1}")]
		public virtual string SecondAddressLine { get; set; }
        
        [DisplayName("Логотип")]
        public virtual string LogoUrl { get; set; }


        /*[DisplayName("Количество дней до заезда")]
        [Range(0, 14, ErrorMessage = "Значение для \"{0}\" должно быть от {1} до {2}")]
		public virtual int DaysUntilCancelEnds { get; set; }
        [DisplayName("Стоимость отмены бронирования")]
        public virtual bool CancelPrice { get; set; }*/

        [DataType(DataType.Time)]
        [DisplayName("Начало регистрации приезда")]
        public virtual DateTime ArrivalTimeStarts { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Конец регистрации приезда")]
        public virtual DateTime ArrivalTimeEnds { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Начало регистрации отъезда")]
        public virtual DateTime DepartureTimeStarts { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Конец регистрации отъезда")]
        public virtual DateTime DepartureTimeEnds { get; set; }

        //Чекбоксы
        [DisplayName("Проверено")]
		public virtual bool Checked { get; set; }
        [DisplayName("Доступно для снятия")]
        public virtual bool Enable { get; set; }
        [DisplayName("Закончено")]
        public virtual bool Finished { get; set; }
        [DisplayName("Залкировано")]
		public virtual bool Bolcked { get; set; }

		[NotMapped]
		[DisplayName("Доступно для поиска")]
		public bool EnableToSearch
		{
			get => Enable && Finished && !Bolcked;
		}

        [DisplayName("Дата создания отеля")]
		public DateTime CreateTimeStamp { get; set; }
		
        public User User { get; set; }
        public ApartmentType ApartmentType { get; set; }
        public Registration Registration { get; set; }
		public Country Country { get; set; }
		public City City { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Number> Numbers { get; set; }
		public List<ApartmentService> ApartmentServices { get; set; }
		public List<Reservation> Reservations { get; set; }
		public List<Review> Reviews { get; set; }

		[NotMapped]
		public decimal? AverageScore
		{
			get
			{
				if (Reviews?.Any(o => o.ReviewScores?.Count > 0) ?? 0 > 0)
					return Math.Round((decimal)Reviews.Average(o => o.ReviewScores.Average(r => r.Value)), 1);
				else
					return null;
			} 
		}

		[NotMapped]
		public List<Score> Scores { get; set; }

		public bool HaveFreeNumbers(DateTime? begin, DateTime? end) =>
			Numbers.Any(o => o.HaveFreeEntities((DateTime)begin, (DateTime)end, Reservations.Where(o => !o.Cencel).ToList()));
    }
}
