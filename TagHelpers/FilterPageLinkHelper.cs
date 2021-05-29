using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingLikeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookingLikeApp.TagHelpers
{
	public class FilterPageLinkHelper : TagHelper
	{
		private IUrlHelperFactory _urlHelperFactory;
		public FilterPageLinkHelper(IUrlHelperFactory urlHelperFactory)
		{
			_urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PageViewModel PageViewModel { get; set; }
		public string PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{

			if(PageViewModel.TotalPages > 1)
			{
				IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
				output.TagName = "div";

				var tag = new TagBuilder("ul");
				tag.AddCssClass("pagination");
				tag.AddCssClass("my-3");

				for (int i = PageViewModel.PageNumber - 4 > 1 ? PageViewModel.PageNumber : 1; i <= (PageViewModel.PageNumber + 4 < PageViewModel.TotalPages ? PageViewModel.PageNumber + 4 : PageViewModel.TotalPages); i++)
				{
					tag.InnerHtml.AppendHtml(CreateTag(i, urlHelper));
				}

				output.Content.AppendHtml(tag);
			}

		}

		private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
		{
			TagBuilder item = new TagBuilder("li");
			TagBuilder link = new TagBuilder("a");

			if (pageNumber == this.PageViewModel.PageNumber)
			{
				item.AddCssClass("active");
			}
			else
			{
				PageUrlValues["page"] = pageNumber;
				link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
			}
			item.AddCssClass("page-item");
			link.AddCssClass("page-link");
			link.InnerHtml.Append(pageNumber.ToString());
			item.InnerHtml.AppendHtml(link);

			return item;
		}
	}
}
