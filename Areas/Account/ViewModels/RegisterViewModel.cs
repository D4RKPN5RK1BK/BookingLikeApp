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
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [DisplayName("Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DisplayName("Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
