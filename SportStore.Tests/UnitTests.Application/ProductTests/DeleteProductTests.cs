using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Products.Commands;
using SportStore.Domain;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.ProductTests
{
    [TestFixture]
    class DeleteProductTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }
        [Test]
        public async Task CanDeleteProduct()
        {
            int id = (await context.Products.AddAsync(new Product())).Entity.Id;
            await context.SaveChangesAsync();
            int productCount = await context.Products.CountAsync();

            var command = CommandFactory.DeleteProduct(id);
            await new DeleteProductRequestHandler(context, mapper).Handle(command);

            Assert.AreEqual(productCount - 1 , await context.Products.CountAsync());
        }

        [Test]
        public async Task CanDelete_CorrectProduct()
        {
            int id = (await context.Products.AddAsync(new Product())).Entity.Id;
            await context.SaveChangesAsync();
            

            var command = CommandFactory.DeleteProduct(id);
            await new DeleteProductRequestHandler(context, mapper).Handle(command);

            var product = await context.Products.FindAsync(id);

            Assert.IsNull(product);
        }

        [Test]
        public void Throws_ProductNotFound()
        {
            int invalidId = context.Products.Count() + 1;

            var command = CommandFactory.DeleteProduct(invalidId);

            Assert.ThrowsAsync<ArgumentException>(async () => 
                                await new DeleteProductRequestHandler(context, mapper).Handle(command));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void ShouldHaveErrors_id_lessThanOne(int id)
        {
            var validator = new DeleteProductValidator();
            var command = CommandFactory.DeleteProduct(id);
            validator.ShouldHaveValidationErrorFor(c => c.ProductId, command);
        }

        [Test]
        public void ShouldNotHaveErrors_id_greaterThanZero()
        {
            var validator = new DeleteProductValidator();
            var command = CommandFactory.DeleteProduct(1);
            validator.ShouldNotHaveValidationErrorFor(c => c.ProductId, command);
        }
    }
}
