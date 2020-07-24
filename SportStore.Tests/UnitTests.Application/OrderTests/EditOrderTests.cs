using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Domain;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class EditOrderTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }

        [Test]
        public async Task CanCreateOrder()
        {
            int id = context.Orders.Add(new Order()).Entity.Id;
            await context.SaveChangesAsync();

            var customer = new CustomerVM() {
                Name = "TestName",
                Adress = new AdressVm()
                {
                    City = "TestCity"
                }
            };
            var command = CommandFactory.EditOrder(id, true, true, customer);

            int actualId = await new EditOrderRequstHandler(context, mapper).Handle(command);

            var order = await context.Orders.Include(o => o.CustomerAdress).FirstOrDefaultAsync(o => o.Id == command.OrderId);

            Assert.IsNotNull(order);
            Assert.AreEqual(command.Customer.Name, order.Name);
            Assert.AreEqual(command.IsGiftWrap, order.GiftWrap);
            Assert.AreEqual(command.Shipped, order.Shipped);
            Assert.AreEqual(command.Customer.Adress.City, order.CustomerAdress.City);
        }

        [Test]
        public void Throws_on_WrongId()
        {
            var command = CommandFactory.EditOrder(-1, true, true, null);
            Assert.ThrowsAsync<ArgumentException>(async () 
                => await new EditOrderRequstHandler(context, mapper).Handle(command));
        }

        [Test]
        public void Creates_CorrectCommand_fromOrder()
        {            
            var customer = new CustomerVM()
            {
                Name = "TestName",
                Adress = new AdressVm()
                {
                    City = "TestCity"
                }
            };

            var order = new OrderVm() { OrderId = 1, Customer = customer, GiftWrap = true, Shipped = false };

            var act = EditOrderCommand.FromOrder(order);

            Assert.AreEqual(order.OrderId, act.OrderId);
            Assert.AreEqual(order.Customer.Name, act.Customer.Name);
            Assert.AreEqual(order.GiftWrap, act.IsGiftWrap);
            Assert.AreEqual(order.Shipped, act.Shipped);
            Assert.AreEqual(order.Customer.Adress.City, act.Customer.Adress.City);

        }

        [Test]
        public void ShouldHaveErrors_Name_IsEmpty()
        {
            var validator = new EditOrderValidator();
            var customer = new CustomerVM()
            {
                Name = " ",
                Adress = new AdressVm()
            };
            var command = CommandFactory.EditOrder(1, true, true, customer);
            validator.ShouldHaveValidationErrorFor(c => c.Customer.Name, command);
        }

        [Test]
        public void ShouldNotHaveErrors_Name_IsSet()
        {
            var validator = new EditOrderValidator();
            var customer = new CustomerVM()
            {
                Name = "TestName",
                Adress = new AdressVm()
            };
            var command = CommandFactory.EditOrder(1, true, true, customer);
            validator.ShouldNotHaveValidationErrorFor(c => c.Customer.Name, command);
        }

        [Test]
        public void ShouldHaveErrors_Country_IsEmpty()
        {
            var validator = new EditOrderValidator();
            var customer = new CustomerVM()
            {
                Name = " ",
                Adress = new AdressVm() { Country = " "}
            };
            var command = CommandFactory.EditOrder(1, true, true, customer);
            validator.ShouldHaveValidationErrorFor(c => c.Customer.Adress.Country, command);
        }

        [Test]
        public void ShouldNotHaveErrors_Country_IsSet()
        {
            var validator = new EditOrderValidator();
            var customer = new CustomerVM()
            {
                Name = " ",
                Adress = new AdressVm() { Country = "Test Country" }
            };
            var command = CommandFactory.EditOrder(1, true, true, customer);
            validator.ShouldNotHaveValidationErrorFor(c => c.Customer.Adress.Country, command);
        }
    }
}
