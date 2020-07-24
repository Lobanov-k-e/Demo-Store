using FluentValidation.TestHelper;
using NUnit.Framework;
using SportStore.Application.Orders.Commands;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    [TestFixture]
    class DeleteOrderTests : TestBase
    {

        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }

        [Test]
        public async Task CanDeleteOrder()
        {
            int id = context.Orders.Add(new Domain.Order()).Entity.Id;
            await context.SaveChangesAsync();

            var command = CommandFactory.DeleteOrder(id);

            int actualId = await new DeleteOrderRequestHandler(context, mapper).Handle(command);

            var order = await context.Orders.FindAsync(id);

            Assert.IsNull(order);
            Assert.AreEqual(id, actualId);
            
        }

        [Test]
        public void Throws_incorectId()
        {
            var command = CommandFactory.DeleteOrder(-1);
            Assert.ThrowsAsync<ArgumentException>(async() 
                => await new DeleteOrderRequestHandler(context, mapper).Handle(command) );
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ShouldHaveErrors_Id_LessThanOne(int id)
        {
            var validator = new DeleteOrderValdator();
            var command = CommandFactory.DeleteOrder(id);
            validator.ShouldHaveValidationErrorFor(c => c.OrderId, command);
        }

        [Test]
        public void ShouldNotHaveErrors_Id_GreaterThanZero()
        {
            var validator = new DeleteOrderValdator();
            var command = CommandFactory.DeleteOrder(1);
            validator.ShouldNotHaveValidationErrorFor(c => c.OrderId, command);
        }
    }
}
