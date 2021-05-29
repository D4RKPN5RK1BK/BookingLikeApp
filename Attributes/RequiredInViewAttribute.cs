using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Attributes
{
	public class RequiredInViewAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return !string.IsNullOrEmpty(value?.ToString() ?? string.Empty);
		}
	}
}
