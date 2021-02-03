using BookingLikeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class PhotosViewModel : Registration
    {
        public List<Photo> ApartmentPhotos { get; set; }

        [DisplayName("Логотип")]
        public string LogoUrl { get; set; }

        public PhotosViewModel() { }

        public PhotosViewModel(Models.Apartment apartment) : base(apartment)
        {
            if (apartment.Photos != null)
            {
                ApartmentPhotos = apartment.Photos.ToList();
            }
        }
    }
}
