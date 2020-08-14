using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Orders.Queries;
using SportStore.Domain;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class GetByIDTests : TestBase
    {

        [Test]
        public async Task CanGetById()
        {
            const int orderId = 341;
            context.Orders.Add(NewOrder(orderId));
            await context.SaveChangesAsync();

            var order = await new GetOrderByIdRequestHandler(context, mapper)
                .Handle(QueryFactory.GetOrderById(orderId));

            Assert.IsNotNull(order);
        }

        [Test]
        public async Task Returns_Null_If_OrderNotExist()
        {
            const int orderId = 421;

            bool exists = (await context.Orders.FindAsync(orderId)) != null;

            var order = await new GetOrderByIdRequestHandler(context, mapper)
                .Handle(QueryFactory.GetOrderById(orderId));

            Assert.IsFalse(exists);
            Assert.IsNull(order);
        }

        private static Order NewOrder(int orderId)
        {
            return new Order() 
            { 
                Id = orderId,
                CustomerAdress = new Adress(),
                GiftWrap = true,
                Shipped = false,
                Name = "Test"           
            };
        }
    }
}
