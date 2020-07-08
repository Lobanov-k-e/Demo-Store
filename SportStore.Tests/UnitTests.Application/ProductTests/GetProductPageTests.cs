using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit;
using NUnit.Framework;
using SportStore.Application.Products.Queries;
using SportStore.Tests.UnitTests.Application;

namespace SportStore.UnitTests.Application.ProductTests
{
    [TestFixture]
    class GetProductPageTests : TestBase
    {
        [Test]
        public async Task CanPaginate()
        {
            int pageNumber = 1;
            int pageSize = 3;

            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);
            int productsCount = context.Products.Count();
            int expectedCount = productsCount > pageSize ?
                pageSize
                : productsCount;
            Assert.NotNull(result as ProductPageVM);
            Assert.IsTrue(result.Products.Count() == expectedCount);
        }

        [Test]
        public async Task Retuns_filters_byCategory()
        {
            int pageNumber = 1;
            int pageSize = 3;

            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize, "Category 1");
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            Assert.That(result.Products.All(p => p.Category.Name == "Category 1"));
        }

        [Test]
        public async Task Returns_correct_currentCategory()
        {
            const string ExpectedCategory = "Category 1";
            var query = queryFactory.GetProductPageQuery(1, 3, ExpectedCategory);
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);
            Assert.That(result.CurrentCategory, Is.EqualTo(ExpectedCategory));            
        }
      
        [Test]
        public async Task NegativeZeroPageNumber_ReturnsFirstPage()
        {
            
            int pageSize = 3;

            int pageNumber = 1;
            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var firstPage= await new GetProductPageQueryHandler(context, mapper).Handle(query);

            pageNumber = 0;
            query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var zeroPage = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            pageNumber = -1;
            query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var negativePage = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            CollectionAssert.AreEqual(firstPage.Products, zeroPage.Products, new ProductDTOByIdComparer());
            CollectionAssert.AreEqual(firstPage.Products, negativePage.Products, new ProductDTOByIdComparer());
        }

        [Test]
        public async Task Returns_CorrectPageInfo()
        {
            int pageNumber = 1;
            int pageSize = 3;
            GetProductPageQuery query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var result = (await new GetProductPageQueryHandler(context, mapper).Handle(query)).PageInfo;

            var expected = new PageInfo()
            {
                CurrentPage = pageNumber,
                ItemsCount = context.Products.Count(),
                ItemsPerPage = pageSize
            };

            Assert.AreEqual(expected.CurrentPage, result.CurrentPage);
            Assert.AreEqual(expected.ItemsPerPage, result.ItemsPerPage);
            Assert.AreEqual(expected.ItemsCount, result.ItemsCount);
            Assert.AreEqual(expected.TotalPages, result.TotalPages);
        }

        [Test]
        public async Task Retuns_allProducts_ifCategoryNameisnull()
        {
            int pageNumber = 1;
            int pageSize = 3;

            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize);
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);
            int expected = context.Products.Count();

            Assert.AreEqual(expected, result.PageInfo.ItemsCount);
        }

        [Test]
        public async Task Retuns_AllCorrectProducts_ifCategoryNameisSet()
        {
            int pageNumber = 1;
            int pageSize = 3;

            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize, "Category 2");
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);
            int expected = context.Products.Count(p => p.Category.Name == "Category 2");
            Assert.AreEqual(expected, result.PageInfo.ItemsCount);
        }



    }
}
