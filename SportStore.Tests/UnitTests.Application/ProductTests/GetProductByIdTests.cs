using NUnit.Framework;
using SportStore.Application.Products.Queries;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.Application.ProductTests
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
        [Test]
        public async Task Returns_null_ifProductNotFound()
        {
            var query = new GetProductByIdQuery() { ProductId = context.Products.Count() + 1 };
            var act = await new GetProductByIdHandler(context, mapper).Handle(query);
            Assert.IsNull(act);
        }
    }
}
