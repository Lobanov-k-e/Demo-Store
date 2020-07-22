using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Products.Queries;
using SportStore.Tests.UnitTests.Application;
using SportStore.UnitTests.UnitTests.Application;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.UnitTests.Application.ProductTests
{
    [TestFixture]
    class GetAllProductsTests : TestBase
    {
        [Test]
        public async Task CanGetProducts()
        {
            var query = QueryFactory.GetAllProducts();

            var act = await new GetAllProductsQueryRequestHandler(context, mapper).Handle(query);

            int expectedCount = await context.Products.CountAsync();

            Assert.AreEqual(expectedCount, act.Count());

        }
    }
}
