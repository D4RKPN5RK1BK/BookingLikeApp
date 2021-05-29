using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingLikeApp.Areas.Apartment.ViewModels
{
	public class NumberBedViewModel
	{
		public NumberBed NumberBed { get; set; }
		public SelectList BedsSelectList { get; set; }

		public NumberBedViewModel(NumberBed numberBed, SelectList bedsSelectList)
		{
			NumberBed = numberBed;
			BedsSelectList = bedsSelectList;
		}

		public NumberBedViewModel(NumberBed numberBed, List<Bed> beds)
		{
			NumberBed = numberBed;
			BedsSelectList = new SelectList(beds, "Id", "Name", numberBed?.Id ?? 0 );
		}
	}
}
