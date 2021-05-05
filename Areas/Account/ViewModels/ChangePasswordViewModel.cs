using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingLikeApp.Areas.Account.ViewModels
{
	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Current password")]
		public string CurrentPassword { get; set; }

		[Required]
		[DisplayName("New password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm password")]
		[Compare("NewPassword", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
