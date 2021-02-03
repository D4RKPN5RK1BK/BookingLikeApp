using BookingLikeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class PaymentViewModel : Registration
    {
        public List<Card> Cards { get; set; }

        [DisplayName("Списывать деньги при помощи карт")]
        public bool UseCards { get; set; }
        [DisplayName("Проверено")]
        public bool Checked { get; set; }
        [DisplayName("Возможно снимать")]
        public bool Enable { get; set; }
        [DisplayName("Успешно зарегистрировано")]
        public bool Finished { get; set; }
        
        public bool PolisherInApaertment { get; set; }

        [DisplayName("Место проживаниия платильщика")]
        public int PolisherStreetId { get; set; }
        [DisplayName("Адресная строка платильщика")]
        public string PolisherSecondAddressLine { get; set; } 

        public PaymentViewModel() { }

        public PaymentViewModel(Models.Apartment apartment) : base(apartment)
        {

        }
    }
}
