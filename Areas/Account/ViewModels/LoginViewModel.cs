using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingLikeApp.Areas.Account.ViewModels
{
	public class LoginViewModel
    {
		[Required(ErrorMessage = "Это поле обязательно для заполнения")]
		[DataType(DataType.Text)]
		[DisplayName("Имя пользователя")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "Это поле обязательно для заполнения")]
		[DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
