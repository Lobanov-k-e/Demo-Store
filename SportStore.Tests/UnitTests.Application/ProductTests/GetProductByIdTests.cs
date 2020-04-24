using NUnit.Framework;
using SportStore.Application.Products.Queries;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.ProductTests
{
    [TestFixture]
    class GetProductByIdTests : TestBase
    {
        [Test]
        public async Task CanGetProduct()
        {
            var query = new GetProductByIdQuery() { ProductId = 1};
            var act = await new GetProductByIdHandler(context, mapper).Handle(query);

            Assert.AreEqual(1, act.Id);
        }
    }
}
