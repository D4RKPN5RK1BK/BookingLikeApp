using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DisplayName("Current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [DisplayName("New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
