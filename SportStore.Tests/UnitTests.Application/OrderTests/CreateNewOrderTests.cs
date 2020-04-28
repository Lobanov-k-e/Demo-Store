using NUnit.Framework;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Application.Products.Queries;
using SportStore.Infrastructure.Migrations;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class CreateNewOrderTests : TestBase
    {
        [Test]
        public async Task CanCreate()
        {
            var orderVm = new OrderVm();
            orderVm.Customer = new CustomerVM() { Name = "test", Adress = new AdressVm() };
            orderVm.GiftWrap = true;
            orderVm.OrderLines = new List<OrderLineVm>()
            {
                new OrderLineVm
                {
                    Product = new ProductDTO(),
                    Quantity = 1

                },
                new OrderLineVm
                {
                    Product = new ProductDTO(),
                    Quantity = 1
                }
            };

            var command = CommandFacory.GetCreateNewOrderCommand(orderVm);
            var result = await new CreateNewOrderHandler(context, mapper).Handle(command);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.Orders.Count());
        }
        [Test]
        public async Task NotCreateProducts()
        {
            var orderVm = new OrderVm();
            orderVm.Customer = new CustomerVM() { Name = "test", Adress = new AdressVm() };
            orderVm.GiftWrap = true;
            orderVm.OrderLines = new List<OrderLineVm>()
            {
                new OrderLineVm
                {
                    Product = new ProductDTO(){ Id = 1},
                    Quantity = 1

                },
                new OrderLineVm
                {
                    Product = new ProductDTO() { Id = 2},
                    Quantity = 1
                }
            };

            int expectedCount = context.Products.Count();

            var command = CommandFacory.GetCreateNewOrderCommand(orderVm);
            await new CreateNewOrderHandler(context, mapper).Handle(command);

            Assert.AreEqual(expectedCount, context.Products.Count());
        }
    }
}
