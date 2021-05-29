using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Attributes;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class BasicInfoViewModel : Models.Apartment
	{
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override string Name { get => base.Name; set => base.Name = value; }
		public override string Description { get => base.Description; set => base.Description = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override int? CountryId { get => base.CountryId; set => base.CountryId = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override int? CityId { get => base.CityId; set => base.CityId = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override string ContactPerson { get => base.ContactPerson; set => base.ContactPerson = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override string ContactPhone { get => base.ContactPhone; set => base.ContactPhone = value; }
		public override string AdditionalPhone { get => base.AdditionalPhone; set => base.AdditionalPhone = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override string SecondAddressLine { get => base.SecondAddressLine; set => base.SecondAddressLine = value; }
	}
}
