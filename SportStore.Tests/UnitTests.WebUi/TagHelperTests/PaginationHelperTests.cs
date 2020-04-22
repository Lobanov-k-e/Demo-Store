using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using NUnit.Framework;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.WebUi
{
    [TestFixture]
    class PaginationHelperTests
    {
        [Test]
        public void Generates_PageLinks()
        {
           
            TagHelperContext ctx;                  
            TagHelperOutput output;

            PaginationHelper paginationHelper = CreatePafinationHelper(out ctx, out output);            

            paginationHelper.Process(ctx, output);
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>"
                          + @"<a href=""Test/Page2"">2</a>"
                          + @"<a href=""Test/Page3"">3</a>",
                          output.Content.GetContent());
        }
        
        [Test]
        public void NotApply_Css_ifPageClassEnabledisFalse()
        {

            TagHelperContext ctx;
            TagHelperOutput output;

            PaginationHelper paginationHelper = CreatePafinationHelper(out ctx, out output);
            paginationHelper.PageClassesEnabled = false;

            paginationHelper.Process(ctx, output);
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>"
                          + @"<a href=""Test/Page2"">2</a>"
                          + @"<a href=""Test/Page3"">3</a>",
                          output.Content.GetContent());
        }
        [Test]
        public void Apply_Css_ifPageClassEnabled()
        {

            TagHelperContext ctx;
            TagHelperOutput output;
           
            PaginationHelper paginationHelper = CreatePafinationHelper(out ctx, out output);
            paginationHelper.PageClassesEnabled = true;
            paginationHelper.PageClass = "testPageClass";
            paginationHelper.PageClassSelected = "selectedPageClass";
            paginationHelper.PageClassNormal = "normalPageClass";

            paginationHelper.Process(ctx, output);
            Assert.IsTrue(output.Content.GetContent().Contains("testPageClass"));
            Assert.IsTrue(output.Content.GetContent().Contains("selectedPageClass"));
            Assert.IsTrue(output.Content.GetContent().Contains("normalPageClass"));
        }
        [Test]
        public void Apply_SelectedCss_toCorrectLink()
        {

            TagHelperContext ctx;
            TagHelperOutput output;
            var pageInfo = new PageInfo
            {
                CurrentPage = 1,
                ItemsPerPage = 8,
                ItemsCount = 20
            };
            PaginationHelper paginationHelper = CreatePafinationHelper(out ctx, out output, pageInfo);
            paginationHelper.PageClassesEnabled = true;            
            paginationHelper.PageClassSelected = "selectedPageClass";           

            paginationHelper.Process(ctx, output);
            Regex match = new Regex(@"<a.*selectedPageClass[^<>]*Page1"); 
            Assert.IsTrue(match.IsMatch(output.Content.GetContent()));
            
        }

        private static PaginationHelper CreatePafinationHelper(out TagHelperContext ctx, out TagHelperOutput output, PageInfo pageInfo = null)
        {
            var urlHelper = new Mock<IUrlHelper>();
            int invocationCount = 0;
            const string ActionName = "Test";
            urlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(() => $"{ActionName}/Page{++invocationCount}");

            var urlHelperFacoty = new Mock<IUrlHelperFactory>();
            urlHelperFacoty.Setup(m => m.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            return new PaginationHelper(urlHelperFacoty.Object)
            {
                PageInfo = pageInfo ?? new PageInfo
                {
                    CurrentPage = 3,
                    ItemsPerPage = 8,
                    ItemsCount = 20
                },
                PageAction = ActionName
            };

            
        }
    }
}
