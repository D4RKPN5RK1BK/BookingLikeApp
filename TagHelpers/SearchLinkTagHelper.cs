using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookingLikeApp.TagHelpers
{
	public class SearchLinkTagHelper : TagHelper
	{
		protected IUrlHelperFactory _urlHelper;
		public SearchLinkTagHelper(IUrlHelperFactory urlHelper)
		{
			_urlHelper = urlHelper;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PageViewModel PageViewModel { get; set; }
		public string PageAction { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = _urlHelper.GetUrlHelper(ViewContext);
			string name = ViewContext?.HttpContext.Request.Query["name"];
			string country = ViewContext?.HttpContext.Request.Query["country"];
			string city = ViewContext?.HttpContext.Request.Query["city"];
			output.TagName = "div";

			TagBuilder tag = new TagBuilder("ul");
			tag.AddCssClass("pagination");

			TagBuilder currentItem = CreateTag(PageViewModel.PageNumber, name, country, city, urlHelper);

			if (PageViewModel.HavePreviousPage)
			{
				TagBuilder prevItem = CreateTag(PageViewModel.PageNumber - 1, name, country, city, urlHelper);
				tag.InnerHtml.AppendHtml(prevItem);
			}

			tag.InnerHtml.AppendHtml(currentItem);

			if (PageViewModel.HaveNextPage)
			{
				TagBuilder nextItem = CreateTag(PageViewModel.PageNumber + 1, name, country, city, urlHelper);
				tag.InnerHtml.AppendHtml(nextItem);
			}

			output.Content.AppendHtml(tag);
		}

		TagBuilder CreateTag(int pageNumber, string name, string country, string city, IUrlHelper urlHelper)
		{
			TagBuilder item = new TagBuilder("li");
			TagBuilder link = new TagBuilder("a");
			if (pageNumber == this.PageViewModel.PageNumber)
			{
				item.AddCssClass("active");
			}
			else
			{
				link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber, name = name, country = country, city = city });
			}

			item.AddCssClass("page-item");
			link.AddCssClass("page-link");

			link.InnerHtml.Append(pageNumber.ToString());
			item.InnerHtml.AppendHtml(link);

			return item;
		}
	}
}
