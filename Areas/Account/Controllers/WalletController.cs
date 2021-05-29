using BookingLikeApp.Data;
using BookingLikeApp.Enums;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Areas.Account.Controllers
{
    [Area("account")]
	[Authorize]
    public class WalletController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;

		public WalletController(ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[HttpGet]
        public async Task<ActionResult> Index()
        {
			User model = await _userManager.GetUserAsync(User);
            return View(model);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> FillWallet(decimal value)
		{
			User user = await _userManager.GetUserAsync(User);
			try
			{
				if (value <= 0)
					ModelState.AddModelError("Wallet", "Транзакция должна быть больше нуля");
				else if (value > 10000000 || value + user.Wallet > decimal.MaxValue)
					ModelState.AddModelError("Wallet", "Транзакция превышает допустимое значение");
				else
				{
					Transaction transaction = new Transaction()
					{
						InputUserId = null,
						OutputUserId = user.Id,
						Value = value,
						TimeStamp = DateTime.Now
					};

					user.Wallet += value;

					_context.Update(user);
					await _context.AddAsync(transaction);
					await _context.SaveChangesAsync();

				}
			}
			catch
			{
				ModelState.AddModelError("Wallet", "Транзакция превышает допустимое значение");
			}				

			return View("Index", user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DrawWallet(decimal value)
		{
			User user = await _userManager.GetUserAsync(User);
			try
			{
				if (value <= 0)
					ModelState.AddModelError("Wallet", "Транзакция должна быть больше нуля");
				else if (user.Wallet - value >= 0 && value < 10000000)
				{
					Transaction transaction = new Transaction()
					{
						InputUserId = user.Id,
						OutputUserId = null,
						Value = value,
						TimeStamp = DateTime.Now
					};

					user.Wallet -= value;

					_context.Update(user);
					await _context.AddAsync(transaction);
					await _context.SaveChangesAsync();
				}	
				else 
					ModelState.AddModelError("Wallet", "Транзакция не должна превышать значение текущего баланса");
			}
			catch
			{
				ModelState.AddModelError("Wallet", "Транзакция не должна превышать значение текущего баланса");
			}

			return View("Index", user);
		}

		[HttpGet]
		public async Task<ActionResult> Transactions(DateTime? begin, DateTime? end,TransactionsSort sortOrder = TransactionsSort.DateAsc, BoolEnum fill = BoolEnum.All, int page = 1)
		{
			User user = await _userManager.GetUserAsync(User);

			TransactionsViewModel model = new TransactionsViewModel()
			{
				Elements = _context.Transactions.Where(o => o.InputUserId == user.Id || o.OutputUserId == user.Id)
				.Include(o => o.InputUser)
				.Include(o => o.OutputUser)
				.ToList(),
				FilterModel = new FilterTransactionsViewModel()
				{
					Begin = begin,
					End = end,
					Fill = fill
				},
				SortModel = new SortTransactionsViewModel(sortOrder),
			};

			if (begin != null && end != null)
				model.Elements = model.Elements.Where(o => o.TimeStamp >= begin && o.TimeStamp <= end).ToList();

			switch(fill)
			{
				case BoolEnum.TrueOnly:
					model.Elements = model.Elements.Where(o => o.InputUserId == user.Id).ToList();
					break;

				case BoolEnum.FalseOnly:
					model.Elements = model.Elements.Where(o => o.OutputUserId == user.Id).ToList();
					break;
			}

			model.PageModel = new PageViewModel(model.Elements.Count, page, 20);
			model.Sort();
			model.Cut();

			return View(model);
		}
	}
}
