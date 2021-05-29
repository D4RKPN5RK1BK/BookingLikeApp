using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookingLikeApp.TagHelpers
{
	public class ProfilePictureTagHelper : TagHelper
	{
		public string ImgUrl { get; set; }
		public int ImageSize { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";
			
			
			var imgCase = new TagBuilder("div");
			if (string.IsNullOrEmpty(ImgUrl))
			{
				var placeholder = new TagBuilder("svg");
				placeholder.Attributes.Add(new KeyValuePair<string, string>("xmlns", "http://www.w3.org/2000/svg"));
				placeholder.Attributes.Add(new KeyValuePair<string, string>("width", ImageSize.ToString()));
				placeholder.Attributes.Add(new KeyValuePair<string, string>("height", ImageSize.ToString()));
				placeholder.Attributes.Add(new KeyValuePair<string, string>("fill", "currentColor"));
				placeholder.Attributes.Add(new KeyValuePair<string, string>("class", "bi bi-person-circle"));
				placeholder.Attributes.Add(new KeyValuePair<string, string>("viewBox", "0 0 16 16"));
				var pathOne = new TagBuilder("path");
				pathOne.Attributes.Add(new KeyValuePair<string, string>("d", "M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z"));
				var pathTwo = new TagBuilder("path");
				pathTwo.Attributes.Add(new KeyValuePair<string, string>("fill-rule", "evenodd"));
				pathTwo.Attributes.Add(new KeyValuePair<string, string>("d", "M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z"));
				placeholder.InnerHtml.AppendHtml(pathOne);
				placeholder.InnerHtml.AppendHtml(pathTwo);
				imgCase.InnerHtml.AppendHtml(placeholder);
			}
			else
			{
				imgCase.AddCssClass("");
				TagBuilder img = new TagBuilder("img");
				img.TagRenderMode = TagRenderMode.SelfClosing;
				img.Attributes.Add(new KeyValuePair<string, string>("src", ImgUrl));
				img.Attributes.Add(new KeyValuePair<string, string>("width", ImageSize.ToString()));
				img.Attributes.Add(new KeyValuePair<string, string>("height", ImageSize.ToString()));
				img.Attributes.Add(new KeyValuePair<string, string>("class", "rounded-circle figure-img"));
				img.Attributes.Add(new KeyValuePair<string, string>("style", "object-fit:cover"));
				imgCase.InnerHtml.AppendHtml(img);
			}

			output.Content.AppendHtml(imgCase);
			
		}
	}
}
