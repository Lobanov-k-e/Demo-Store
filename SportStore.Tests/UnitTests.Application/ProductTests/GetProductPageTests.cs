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
            var queryFactory = new ProductQueryFactory(_context);
            int pageNumber = 1;
            int pageSize = 3;          
            
            var query = queryFactory.GetProductPageQuery(pageNumber, pageSize);

            var act = await query.Execute();            

            Assert.IsTrue(act.Count() == 3);            
        }

        [Test]
        public async Task NegativeZeroPageNumber_ReturnsFirstPage()
        {
            var queryFactory = new ProductQueryFactory(_context);
            int pageNumber = 1;
            int pageSize = 4;

            var firstPage = await queryFactory.GetProductPageQuery(pageNumber, pageSize).Execute();          
            var negativePage = await queryFactory.GetProductPageQuery(-1, pageSize).Execute();             
            var zeroPage = await queryFactory.GetProductPageQuery(0, pageSize).Execute();

            CollectionAssert.AreEqual(firstPage, negativePage, new ProductDTOByIdComparer());
            CollectionAssert.AreEqual(firstPage, zeroPage, new ProductDTOByIdComparer());            
        }

     
    }
}
