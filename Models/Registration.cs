using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingLikeApp.Models
{
    public class Registration
    {
        [Key]
        public int ApartmentId { get; set; }
        public bool BasicInfo { get; set; }
        public bool Numbers { get; set; }
        public bool Rules { get; set; }
        public bool FacilitesRequired { get; set; }
        public bool Facilites { get; set; }
        public bool Photos { get; set; }
        public bool Payment { get; set; }
        public bool Services { get; set; }

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
    }
}
