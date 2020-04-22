﻿using Microsoft.AspNetCore.Mvc;
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

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public PageInfo PageInfo { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }      
        public string PageClassNormal{ get; set; }
        public string PageClassSelected { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("div");
            for (int i = 1; i < PageInfo.TotalPages + 1; i++)
            {
                var tag = new TagBuilder("a");
                PageUrlValues["pageNumber"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
