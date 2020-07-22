using NUnit.Framework;
using SportStore.Application.Orders;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class GetAllOrdersTest :TestBase
    {
        [Test]
        public async Task CanGetAllOrders()
        {
            var query = QueryFactory.GetAllOrders();
            var result = await new GetAllOrdersRequestHandler(context, mapper).Handle(query);

            int expected = context.Orders.Count();
            Assert.AreEqual(expected, result.Count());
        }
    }
}
