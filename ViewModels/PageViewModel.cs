using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.ViewModels
{
	public class PageViewModel
	{
		public int PageSize { get; private set; }
		public int PageNumber { get; private set; }
		public int TotalPages { get; private set; }

		public PageViewModel(int count, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		}

		public bool HavePreviousPage
		{
			get => PageNumber > 1;
		}

		public bool HaveNextPage
		{
			get => PageNumber < TotalPages;
		}
	}
}
