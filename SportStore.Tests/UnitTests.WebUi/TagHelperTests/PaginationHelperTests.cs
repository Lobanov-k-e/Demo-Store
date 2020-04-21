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
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.WebUi
{
    [TestFixture]
    class PaginationHelperTests
    {
        [Test]
        public void Generates_PageLins()
        {
            var urlHelper = new Mock<IUrlHelper>();
            int invocationCount = 0;
            const string ActionName = "Test";
            urlHelper.Setup(m => m.Action(It.IsAny<UrlActionContext>()))
                .Returns(() => $"{ActionName}/Page{++invocationCount}");

            var urlHelperFacoty = new Mock<IUrlHelperFactory>();
            urlHelperFacoty.Setup(m=>m.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            
            PaginationHelper paginationHelper = new PaginationHelper(urlHelperFacoty.Object)
            {
                PageInfo = new PageInfo
                {
                    CurrentPage = 3,
                    ItemsPerPage = 8,
                    ItemsCount = 20
                },
                PageAction = ActionName
            };

            TagHelperContext ctx = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            paginationHelper.Process(ctx, output);
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>" 
                          + @"<a href=""Test/Page2"">2</a>" 
                          + @"<a href=""Test/Page3"">3</a>",
                          output.Content.GetContent());
        }
    }
}
