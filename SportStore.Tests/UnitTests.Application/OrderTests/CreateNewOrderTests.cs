using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Application.Products.Queries;
using SportStore.Domain;
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

        [SetUp]
        public void SetUp()
        {
            const bool AsNoTracking = true;
            CreateNewContext(AsNoTracking);
        }

        public CreateNewOrderTests() : base(true)
        {
        }

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

        [Test]
        public async Task CreatesCorrectOrder()
        {
            var orderVm = new OrderVm();
            orderVm.Customer = new CustomerVM() { Name = "test", Adress = CreateSampleAdress() };
            orderVm.GiftWrap = true;
            orderVm.OrderLines = new List<OrderLineVm>()
            {
                new OrderLineVm
                {
                    Product = new ProductDTO(){ Id = 1},
                    Quantity = 9

                },
                new OrderLineVm
                {
                    Product = new ProductDTO() { Id = 2},
                    Quantity = 4
                }
            };

            int expectedCount = context.Products.Count();

            var command = CommandFacory.GetCreateNewOrderCommand(orderVm);
            var resultId = await new CreateNewOrderHandler(context, mapper).Handle(command);

            var result = context.Orders.Include(o => o.OrderLines).Single(o => o.Id == resultId);

            Assert.AreEqual(orderVm.Customer.Name, result.Name);
            Assert.IsTrue(result.CustomerAdress.CompareToVm(orderVm.Customer.Adress));
            Assert.AreEqual(orderVm.GiftWrap, result.GiftWrap);
            Assert.AreEqual(orderVm.OrderLines.Count(), result.OrderLines.Count());
            Assert.AreEqual(9, result.OrderLines.Single(p => p.Id == 1).Quantity);
            Assert.AreEqual(4, result.OrderLines.Single(p => p.Id == 2).Quantity);
                        
        }

        private static AdressVm CreateSampleAdress()
        {
            return new AdressVm()
            {
                Line1 = "TestLine1",
                Line2 = "TestLine2",
                Line3 = "TestLine3",
                Country = "TestCountry",
                State = "TestState",
                City = "TestCity",
                Zip = "8888"
            };
        }       
    }
}
