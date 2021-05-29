using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Enums;
using BookingLikeApp.Models;

namespace BookingLikeApp.ViewModels
{
	public class TransactionsViewModel
	{
		public List<Transaction> Elements { get; set; }
		public PageViewModel PageModel { get; set; }
		public FilterTransactionsViewModel FilterModel { get; set; }
		public SortTransactionsViewModel SortModel { get; set; }

		public void Sort()
		{
			switch (SortModel.Current)
			{
				case TransactionsSort.DateAsc:
					Elements = Elements.OrderBy(o => o.TimeStamp).ToList();
					break;
				case TransactionsSort.DateDesc:
					Elements = Elements.OrderByDescending(o => o.TimeStamp).ToList();
					break;
				case TransactionsSort.ValueAsc:
					Elements = Elements.OrderBy(o => o.Value).ToList();
					break;
				case TransactionsSort.ValueDesc:
					Elements = Elements.OrderByDescending(o => o.Value).ToList();
					break;
			}
		}

		public void Cut() =>
			Elements = Elements.Skip(PageModel.PageNumber * PageModel.PageSize - PageModel.PageSize).Take(PageModel.PageSize).ToList();
	}

	

	public class FilterTransactionsViewModel
	{
		public BoolEnum Fill { get; set; }
		public BoolEnum Others { get; set; }
		public DateTime? Begin { get; set; }
		public DateTime? End { get; set; }
	}

	public enum TransactionsSort
	{
		DateAsc,
		DateDesc,
		ValueAsc,
		ValueDesc
	}

	public class SortTransactionsViewModel
	{
		public TransactionsSort Date { get; set; }
		public TransactionsSort Value { get; set; }
		public TransactionsSort Current { get; set; }

		public SortTransactionsViewModel(TransactionsSort sortOrder)
		{
			Date = sortOrder == TransactionsSort.DateAsc ? TransactionsSort.DateAsc : TransactionsSort.DateDesc;
			Value = sortOrder == TransactionsSort.ValueAsc ? TransactionsSort.ValueAsc : TransactionsSort.ValueDesc;
			Current = sortOrder;
		}
	}
}
