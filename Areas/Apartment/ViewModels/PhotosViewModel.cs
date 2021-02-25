using BookingLikeApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
    public class PhotosViewModel : Registration
    {
		public int Id { get; set; }
		public List<Photo> ApartmentPhotos { get; set; }

        [DisplayName("Логотип")]
        public string LogoUrl { get; set; }

		[DisplayName("Файл")]
		public IFormFile File { get; set; }

        public PhotosViewModel() { }

        public PhotosViewModel(Models.Apartment apartment) : base(apartment)
        {
			Id = apartment.Id;
			if (apartment.Photos != null)
            {
                ApartmentPhotos = apartment.Photos.ToList();
            }
        }
    }
}
