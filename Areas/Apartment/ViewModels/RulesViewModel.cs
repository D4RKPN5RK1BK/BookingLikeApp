using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Attributes;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class RulesViewModel : Models.Apartment
	{
		/*[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override int DaysUntilCancelEnds { get => base.DaysUntilCancelEnds; set => base.DaysUntilCancelEnds = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override bool CancelPrice { get => base.CancelPrice; set => base.CancelPrice = value; }*/

		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override DateTime ArrivalTimeStarts { get => base.ArrivalTimeStarts; set => base.ArrivalTimeStarts = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override DateTime ArrivalTimeEnds { get => base.ArrivalTimeEnds; set => base.ArrivalTimeEnds = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override DateTime DepartureTimeStarts { get => base.DepartureTimeStarts; set => base.DepartureTimeStarts = value; }
		[RequiredInView(ErrorMessage = "Значение поля \"{0}\" является обязательным для заполнения")]
		public override DateTime DepartureTimeEnds { get => base.DepartureTimeEnds; set => base.DepartureTimeEnds = value; }
	}
}
