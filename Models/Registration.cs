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
        public virtual bool Photos { get; set; }
        public virtual bool Services { get; set; }
		
		/*public virtual bool BasicInfoRequired { get; set; }
        public virtual bool NumbersRequired { get; set; }
        public virtual bool RulesRequired { get; set; }
        public virtual bool FacilitesRequired { get; set; }
        public virtual bool PhotosRequired { get; set; }
        public virtual bool PaymentRequired { get; set; }
        public virtual bool ServicesRequired { get; set; }*/

        public Apartment Apartment { get; set; }

        [NotMapped]
        public bool IsFinished
        {
            get
            {
				return  BasicInfo &&
					Numbers &&
					Services &&
					Photos &&
					Rules;
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
                if (!Rules) return nameof(Rules);
                return string.Empty;
            }
        }

        [NotMapped]
        public virtual Registration RegProps
        {
            set
            {
				if (value == null)
				{
					BasicInfo = false;
					Numbers = false;
					Rules = false;
					Services = false;
					Photos = false;
				}
				else
				{
					BasicInfo = value.BasicInfo;
					Numbers = value.Numbers;
					Rules = value.Rules;
					Services = value.Services;
					Photos = value.Photos;
				}                
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
                    {nameof(Numbers), Numbers},
                    {nameof(Rules), Rules},
                    {nameof(Photos), Photos},
                    {nameof(Services), Services},
                };
            }
        }

		/*[NotMapped]
		public string ApartmentName { get; set; }


        public Registration() { }

        public Registration(Apartment apartment)
        {
			ApartmentName = apartment.Name;
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
            if (model == null)
			{
				BasicInfo = false;
				Numbers = false;
				Rules = false;
				Services = false;
				Facilites = false;
				Payment = false;
				Photos = false;
			}
			else
			{
				BasicInfo = model.BasicInfo;
				Numbers = model.Numbers;
				Rules = model.Rules;
				Services = model.Services;
				Facilites = model.Facilites;
				Payment = model.Payment;
				Photos = model.Photos;
			}
        }*/
	}
}
