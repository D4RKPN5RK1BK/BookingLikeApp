using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.ViewModels
{
    public class RegisterViewModel
    {
		[Required(ErrorMessage = "Это поле обязательно для заполнения")]
		[DisplayName("Имя пользователя")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "Это поле обязательно для заполнения")]
		[DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
		[Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [DisplayName("Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
