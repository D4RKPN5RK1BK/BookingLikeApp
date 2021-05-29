using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.Areas.Admin.ViewModels;
using BookingLikeApp.Data;
using BookingLikeApp.Models;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingLikeApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Authorize(Roles = "staff")]
	public class UsersController : Controller
	{
		protected readonly ApplicationDbContext _context;
		protected readonly UserManager<User> _userManager;
		protected const int PageSize = 20;

		public UsersController( ApplicationDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public ActionResult Index(DateTime? registrationDateBegin, DateTime? registrationDateEnd, int page = 1, string name = "", bool withApartments = false, SortState sortOrder = SortState.NameAsc)
		{
			if (registrationDateBegin == null)
				registrationDateBegin = DateTime.MinValue;

			if (registrationDateEnd == null)
				registrationDateEnd = DateTime.Now;

			List<User> users;
			var filterModel = new FilterUsersViewModel(name, registrationDateBegin, registrationDateEnd, withApartments);


			if (string.IsNullOrEmpty(name))
				users = _context.Users.ToList();
			else
				if (_context.Users.Any(o => o.UserName == name))
					users = _context.Users.Where(o => o.UserName == name).ToList();
				else
					users = _context.Users.Where(o => o.UserName.Contains(name)).ToList();

			users = users.Where(o => o.RegistationDate >= registrationDateBegin && o.RegistationDate <= registrationDateEnd).ToList();

			if (withApartments)
				users = users.Where(o => _context.Apartments.Count(a => a.UserId == o.Id) > 0).ToList();

			switch (sortOrder)
			{
				case SortState.NameAsc:
					users = users.OrderBy(o => o.UserName).ToList();
					break;
				case SortState.NameDesc:
					users = users.OrderByDescending(o => o.UserName).ToList();
					break;
				case SortState.RegistrationDateAsc:
					users = users.OrderBy(o => o.RegistationDate).ToList();
					break;
				case SortState.RegistrationDateDesc:
					users = users.OrderByDescending(o => o.RegistationDate).ToList();
					break;
				case SortState.CountAsc:
					users = users.OrderBy(o => _context.Apartments.Count(a => a.UserId == o.Id)).ToList();
					break;
				case SortState.CountDesc:
					users = users.OrderByDescending(o => _context.Apartments.Count(a => a.UserId == o.Id)).ToList();
					break;
				default:
					users = users.OrderBy(o => o.UserName).ToList();
					break;
			}

			PageViewModel pageModel = new PageViewModel(users.Count, page, PageSize);
			users = users.Skip((page - 1) * PageSize).Take(PageSize).ToList();

			var model = new SearchUsersViewModel()
			{
				FilterUserViewModel = filterModel,
				SortUsersViewModel = new SortUsersViewModel(sortOrder),
				PageViewModel = pageModel,
				Users = users
			};
			
			return View(model);
		}

		public async Task<ActionResult> Read(string id)
		{
			User user = await _userManager.FindByIdAsync(id);
			UserInfoViewModel model = new UserInfoViewModel()
			{
				User = user,
				UserRoles = (List<string>)await _userManager.GetRolesAsync(user)
			};
			return View(model);
		}

		public async Task<ActionResult> Apartments(string id, int page = 1)
		{
			UserInfoViewModel model = new UserInfoViewModel()
			{
				User = await _userManager.FindByIdAsync(id)
			};
			model.SearchApartments = new ApartmentsViewModel()
			{
				Apartments = _context.Apartments.Where(a => a.UserId == id).ToList(),
				FilterModel = new FilterApartmentsViewModel(),
				SortOrder = new SortApartmentsViewModel(SortApartments.NameAsc)
				
			};

			model.SearchApartments.Filter();
			model.SearchApartments.Page = new PageViewModel(model.SearchApartments.Apartments.Count, page, PageSize);

			model.SearchApartments.Sort();
			model.SearchApartments.Cut();

			return View(model);
		}

		public async Task<ActionResult> Reservations(string id)
		{
			UserInfoViewModel model = new UserInfoViewModel()
			{
				User = await _userManager.FindByIdAsync(id)
			};
			return View(model);
		}

		public async Task<ActionResult> Reviews(string id)
		{
			UserInfoViewModel model = new UserInfoViewModel()
			{
				User = await _userManager.FindByIdAsync(id)
			};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult> AddRoleStaff(string id)
		{
			User user = await _context.Users.FindAsync(id);
			await _userManager.AddToRoleAsync(await _context.Users.FindAsync(id), "staff");
			return RedirectToAction("Read", new { id });
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult> RemoveRoleStaff(string id)
		{
			await _userManager.RemoveFromRoleAsync(await _context.Users.FindAsync(id), "staff");
			return RedirectToAction("Read", new { id });
		}

		[HttpPost]
		[Authorize(Roles = "admin, staff")]
		public async Task<ActionResult> Block(string id, DateTime? date)
		{
			User user = await _context.Users.FindAsync(id);
			if (await _userManager.GetLockoutEnabledAsync(user) && date != null && date > DateTime.Now)
				await _userManager.SetLockoutEndDateAsync(user, date);
			return RedirectToAction("Read", new { id });
		}

		[HttpPost]
		[Authorize(Roles = "admin, staff")]
		public async Task<ActionResult> Unblock(string id)
		{
			User user = await _context.Users.FindAsync(id);
			if(await _userManager.IsLockedOutAsync(user))
				await _userManager.SetLockoutEndDateAsync(user, null);
			return RedirectToAction("Read", new { id });
		}
	}
}
