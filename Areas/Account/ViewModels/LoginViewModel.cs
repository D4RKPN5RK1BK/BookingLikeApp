using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingLikeApp.Areas.Account.ViewModels
{
	public class LoginViewModel
    {
        [DataType(DataType.Text)]
		[DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
		[DisplayName("Пароль")]
        public string Password { get; set; }

		[DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
