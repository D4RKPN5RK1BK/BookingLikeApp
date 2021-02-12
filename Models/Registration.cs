using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingLikeApp.Models
{
    public class Registration
    {
        [Key]
        public int ApartmentId { get; set; }
        public virtual bool BasicInfo { get; set; }
        public virtual bool Numbers { get; set; }
        public virtual bool Rules { get; set; }
        public virtual bool FacilitesRequired { get; set; }
        public virtual bool Facilites { get; set; }
        public virtual bool Photos { get; set; }
        public virtual bool Payment { get; set; }
        public virtual bool Services { get; set; }

        public Apartment Apartment { get; set; }

        [NotMapped]
        public bool IsFinished
        {
            get
            {
                if (FacilitesRequired)
                    return (BasicInfo && Numbers && Rules && Facilites && Photos && Payment && Services);
                else
                    return (BasicInfo && Numbers && Rules && Photos && Payment && Services);
            }
        }

        [NotMapped]
        public string Unfinished
        {
            get
            {
                if (!BasicInfo) return nameof(BasicInfo);
                if (!Numbers) return nameof(Numbers);
                if (!Services) return nameof(Services);
                if (!Photos) return nameof(Photos);
                if (!Facilites && !FacilitesRequired) return nameof(Facilites);
                if (!Rules) return nameof(Rules);
                if (!Payment) return nameof(Payment);
                return string.Empty;
            }
        }

        [NotMapped]
        public virtual Registration RegProps
        {
            set
            {
                if (value == null) return;
                BasicInfo = value.BasicInfo;
                Numbers = value.Numbers;
                Rules = value.Rules;
                Services = value.Services;
                Facilites = value.Facilites;
                Payment = value.Payment;
                Photos = value.Photos;
            }
        }

        [NotMapped]
        public Dictionary<string, bool> FinishedDictionary
        {
            get
            {
                return new Dictionary<string, bool>()
                {
                    {nameof(BasicInfo), BasicInfo},
                    {nameof(Numbers), BasicInfo},
                    {nameof(Rules), BasicInfo},
                    {nameof(Facilites), BasicInfo},
                    {nameof(Photos), BasicInfo},
                    {nameof(Payment), BasicInfo},
                    {nameof(Services), BasicInfo},
                };
            }
        }


        public Registration() { }

        public Registration(Models.Apartment apartment)
        {
            if (apartment.Registration != null)
            {
                BasicInfo = apartment.Registration.BasicInfo;
                Numbers = apartment.Registration.Numbers;
                Rules = apartment.Registration.Rules;
                Services = apartment.Registration.Services;
                Facilites = apartment.Registration.Facilites;
                Payment = apartment.Registration.Payment;
                Photos = apartment.Registration.Photos;
            }
        }

        public virtual void SetProps(Registration model)
        {
            if (model == null) return;
            BasicInfo = model.BasicInfo;
            Numbers = model.Numbers;
            Rules = model.Rules;
            Services = model.Services;
            Facilites = model.Facilites;
            Payment = model.Payment;
            Photos = model.Photos;
        }
    }
}
