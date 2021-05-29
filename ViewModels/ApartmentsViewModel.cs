using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Enums;

namespace BookingLikeApp.ViewModels
{
	public class ApartmentsViewModel
	{
		public PageViewModel Page { get; set; }
		public FilterApartmentsViewModel FilterModel { get; set; }
		public SortApartmentsViewModel SortOrder { get; set; }
		public List<Models.Apartment> Apartments { get; set; }

		public ApartmentsViewModel()
		{
			FilterModel = new FilterApartmentsViewModel()
			{
				Finished = BoolEnum.TrueOnly,
				Enabled = BoolEnum.TrueOnly,
				Checked = BoolEnum.All,
				Name = "",
				ReservationTimeBegin = DateTime.Now,
				ReservationTimeEnd = DateTime.Now.AddDays(7),
				CountryId = 0,
				CityId = 0,
				MinRating = 0
			};
			SortOrder = new SortApartmentsViewModel(SortApartments.NameAsc);
		}

		public void FilterName()
		{
			if(!string.IsNullOrEmpty(FilterModel.Name))
				if (Apartments.Any(o => o.Name.ToLower() == FilterModel.Name.ToLower()))
					Apartments = Apartments.Where(o => o.Name.ToLower() == FilterModel.Name.ToLower()).ToList();
				else 
					Apartments = Apartments.Where(o => o.Name.ToLower().Contains(FilterModel.Name.ToLower())).ToList();
		}

		public void Filter()
		{
			if(!string.IsNullOrEmpty(FilterModel.Name))
				Apartments = Apartments.Where(o => o.Name == FilterModel.Name).ToList();

			if (FilterModel.MinRating != null && FilterModel.MinRating > 0)
				Apartments = Apartments.Where(a => a.Reviews.Average(r => r.ReviewScores.Average(o => o.Value)) >= FilterModel.MinRating).ToList();

			if(FilterModel.CityId != null && FilterModel.CityId > 0)
				Apartments = Apartments.Where(a => a.CityId == FilterModel.CityId).ToList();

			if (FilterModel.CountryId != null && FilterModel.CountryId > 0)
				Apartments = Apartments.Where(a => a.CountryId == FilterModel.CountryId).ToList();

			switch(FilterModel.Finished)
			{
				case BoolEnum.TrueOnly:
					Apartments = Apartments.Where(o => o.Finished == true).ToList();
					break;

				case BoolEnum.FalseOnly:
					Apartments = Apartments.Where(o => o.Finished == false).ToList();
					break;
			}

			switch (FilterModel.Enabled)
			{
				case BoolEnum.TrueOnly:
					Apartments = Apartments.Where(o => o.Enable == true).ToList();
					break;

				case BoolEnum.FalseOnly:
					Apartments = Apartments.Where(o => o.Enable == false).ToList();
					break;
			}

			switch (FilterModel.Checked)
			{
				case BoolEnum.TrueOnly:
					Apartments = Apartments.Where(o => o.Checked == true).ToList();
					break;

				case BoolEnum.FalseOnly:
					Apartments = Apartments.Where(o => o.Checked == false).ToList();
					break;
			}

			if(FilterModel.ReservationTimeBegin != null && FilterModel.ReservationTimeEnd != null)
				Apartments = Apartments.Where(o => o.HaveFreeNumbers((DateTime)FilterModel.ReservationTimeBegin, (DateTime)FilterModel.ReservationTimeEnd)).ToList();
		}

		public void Sort()
		{
			switch (SortOrder.Current)
			{
				case SortApartments.NameAsc:
					Apartments = Apartments.OrderBy(o => o.Name).ToList();
					break;
				case SortApartments.NameDesc:
					Apartments = Apartments.OrderByDescending(o => o.Name).ToList();
					break;
				case SortApartments.RatingAsc:
					Apartments = Apartments
						.OrderBy(a => a.Reviews
							.Where(o => o.ReviewScores.Count > 0)
							.Average(o => o.ReviewScores?
								.Average(r => r.Value)))
						.ToList();
					break;
				case SortApartments.RatingDesc:
					Apartments = Apartments
						.OrderByDescending(a => a.Reviews
							.Where(o => o.ReviewScores.Count > 0)
							.Average(o => o.ReviewScores?
								.Average(r => r.Value)))
						.ToList();
					break;
				case SortApartments.ReservationsAsc:
					Apartments = Apartments.OrderBy(o => o.Reservations.Count).ToList();
					break;
				case SortApartments.ReservationsDesc:
					Apartments = Apartments.OrderByDescending(o => o.Reservations.Count).ToList();
					break;
				case SortApartments.ReviewsAsc:
					Apartments = Apartments.OrderBy(o => o.Reviews.Count).ToList();
					break;
				case SortApartments.ReviewsDesc:
					Apartments = Apartments.OrderByDescending(o => o.Reviews.Count).ToList();
					break;
				default:
					Apartments = Apartments.OrderBy(o => o.Name).ToList();
					break;
			}
		}

		public void Cut() =>
			Apartments = Apartments.Skip(Page.PageNumber * Page.PageSize - Page.PageSize).Take(Page.PageSize).ToList();
	}

	public class FilterApartmentsViewModel
	{
		public DateTime? ReservationTimeBegin { get; set; }
		public DateTime? ReservationTimeEnd { get; set; }
		public int? CountryId { get; set; }
		public int? CityId { get; set; }
		public int? ApartmentTypeId { get; set; }
		public string Name { get; set; }
		public double? MinRating { get; set; }
		public BoolEnum Finished { get; set; }
		public BoolEnum Enabled { get; set; }
		public BoolEnum Checked { get; set; }
	}

	public enum SortApartments
	{
		NameAsc,
		NameDesc,
		RatingAsc,
		RatingDesc,
		ReservationsAsc,
		ReservationsDesc,
		ReviewsAsc,
		ReviewsDesc,
	}

	public class SortApartmentsViewModel
	{
		public SortApartments Name { get; private set; }
		public SortApartments Rating { get; private set; }
		public SortApartments Reservations { get; private set; }
		public SortApartments Reviews { get; private set; }
		public SortApartments Current { get; private set; }

		public SortApartmentsViewModel(SortApartments sortOrdeer)
		{
			Name = sortOrdeer == SortApartments.NameAsc ? SortApartments.NameAsc : SortApartments.NameDesc;
			Rating = sortOrdeer == SortApartments.RatingAsc ? SortApartments.RatingAsc : SortApartments.RatingDesc;
			Reservations = sortOrdeer == SortApartments.ReservationsAsc ? SortApartments.ReservationsAsc : SortApartments.ReservationsDesc;
			Reviews = sortOrdeer == SortApartments.ReviewsAsc ? SortApartments.ReviewsAsc : SortApartments.ReviewsDesc;
			Current = sortOrdeer;
		}
	}
}
