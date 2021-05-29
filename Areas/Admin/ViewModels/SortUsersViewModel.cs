using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Admin.ViewModels
{
	public enum SortState
	{
		NameAsc,
		NameDesc,
		RegistrationDateAsc,
		RegistrationDateDesc,
		CountAsc,
		CountDesc
	}

	public class SortUsersViewModel
	{
		public SortState NameSort { get; private set; }
		public SortState RegistrationSort { get; private set; }
		public SortState CountSort { get; private set; }

		public SortState Current { get; private set; }

		public SortUsersViewModel(SortState sortOrder)
		{
			NameSort = sortOrder == SortState.NameAsc ? SortState.NameAsc : SortState.NameDesc;
			RegistrationSort = sortOrder == SortState.RegistrationDateAsc ? SortState.RegistrationDateAsc : SortState.RegistrationDateDesc;
			CountSort = sortOrder == SortState.CountAsc ? SortState.CountAsc : SortState.CountDesc;
			Current = sortOrder;
		}
	}
}
