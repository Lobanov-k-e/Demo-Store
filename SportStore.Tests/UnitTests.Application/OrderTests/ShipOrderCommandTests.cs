using NUnit.Framework;
using SportStore.Application.Orders.Commands;
using SportStore.Infrastructure.Migrations;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class ShipOrderCommandTests : TestBase
    {
        [Test]
        public async Task CanShip()
        {
            int orderId = context.Orders.Add(new Domain.Order() { Shipped = false }).Entity.Id;
            await context.SaveChangesAsync();

            var command = CommandFactory.ShipOrderCommand(orderId);

            await new ShipOrderRequestHandler(context, mapper).Handle(command);

            var order = await context.Orders.FindAsync(orderId);

            Assert.IsTrue(order.Shipped);

        }
    }
}
