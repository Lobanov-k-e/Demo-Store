using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit;
using NUnit.Framework;
using SportStore.Application.Products.Queries;

namespace SportStore.Tests.UnitTests.Application.ProductTests
{
    [TestFixture]
    class GetProductPageTests : TestBase
    {      
        [Test]
        public async Task CanPaginate()
        {
            int pageNumber = 1;
            int pageSize = 3;
            
            var query = new GetProductPageQuery(pageNumber, pageSize);
            var result = await new GetProductPageQueryHandler(context, mapper).Handle(query);
            Assert.NotNull(result as ProductPageVM);
            Assert.IsTrue(result.Products.Count() == pageSize);                      
        }       

        [Test]
        public async Task NegativeZeroPageNumber_ReturnsFirstPage()
        {
            
            int pageSize = 3;

            int pageNumber = 1;
            var query = new GetProductPageQuery(pageNumber, pageSize);
            var firstPage= await new GetProductPageQueryHandler(context, mapper).Handle(query);

            pageNumber = 0;
            query = new GetProductPageQuery(pageNumber, pageSize);
            var zeroPage = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            pageNumber = -1;
            query = new GetProductPageQuery(pageNumber, pageSize);
            var negativePage = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            CollectionAssert.AreEqual(firstPage.Products, zeroPage.Products, new ProductDTOByIdComparer());
            CollectionAssert.AreEqual(firstPage.Products, negativePage.Products, new ProductDTOByIdComparer());
        }

        [Test]
        public async Task Returns_CorrectPageInfo()
        {
            int pageNumber = 1;
            int pageSize = 3;

            var query = new GetProductPageQuery(pageNumber, pageSize);
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

     
    }
}
