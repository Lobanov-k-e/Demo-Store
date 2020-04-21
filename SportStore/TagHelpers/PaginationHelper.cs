using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlFactory;

        public PaginationHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlFactory = urlHelperFactory;
        }

        [ViewContext] [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set;} 
        public PageInfo PageInfo { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("div");
            for (int i = 1; i < PageInfo.TotalPages + 1; i++)
            {
                var tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNumber = i});
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
