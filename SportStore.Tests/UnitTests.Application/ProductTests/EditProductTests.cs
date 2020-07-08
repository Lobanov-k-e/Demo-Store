using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Products.Commands;
using SportStore.Application.Products.Queries;
using SportStore.Tests.UnitTests.Application;
using SportStore.UnitTests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.Application.ProductTests
{
    [TestFixture]
    internal class EditProductTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }

        [Test]
        public async Task CanEditProduct()
        {
            int categoryId = context.Categories.First().Id;
            int productId = context.Products.Add(new Domain.Product() { CategoryId = categoryId }).Entity.Id;
            context.SaveChanges();

            const string Name = "Name";
            const string Description = "Description";
            const decimal Price = 189m;
            int catId = context.Categories.Last().Id;
            var command = CommandFactory.EditProductCommand(productId, Name, Description, catId, Price);

            int actualId = await new EditProductRequestHandler(context, mapper).Handle(command);
            var actualProduct = await context.Products.FindAsync(actualId);

            Assert.IsNotNull(actualProduct);
            Assert.AreEqual(command.Name, actualProduct.Name);
            Assert.AreEqual(command.Price, actualProduct.Price);
            Assert.AreEqual(command.Description, actualProduct.Description);
            Assert.AreEqual(command.CategoryId, actualProduct.CategoryId);
        }

        [Test]
        public async Task Throws_onWrongId()
        {
            int wrongId = await context.Products.CountAsync() + 1;
            var command = CommandFactory.EditProductCommand(wrongId, "Name", "Description", 1, 189m);

            Assert.ThrowsAsync<ArgumentException>(async () => await new EditProductRequestHandler(context, mapper).Handle(command));
        }

        [Test]
        public void CanCreateCommand_From_ProductDTO()
        {
            var productDTO = new ProductDTO()
            {
                Id = 1,
                Name = "Test",
                Description = "TestDescr",
                CategoryId = 12,
                Price = 142m
            };

            var actual = EditProductCommand.FromProduct(productDTO);

            Assert.AreEqual(productDTO.Id, actual.Id);
            Assert.AreEqual(productDTO.Name, actual.Name);
            Assert.AreEqual(productDTO.Price, actual.Price);
            Assert.AreEqual(productDTO.Description, actual.Description);
            Assert.AreEqual(productDTO.CategoryId, actual.CategoryId);
        }


        [Test]
        public void ShouldHaveErrors_Name_IsEmpty()
        {
            var command = CommandFactory.EditProductCommand(1, " ", "Description", 1, 189m);
            var validator = new EditProductCommandValidator();
            validator.ShouldHaveValidationErrorFor(c => c.Name, command);
        }

        [Test]
        public void ShouldNotHaveErrors_Name_IsSpecified()
        {
            var command = CommandFactory.EditProductCommand(1, "Name", "Description", 1, 189m);
            var validator = new EditProductCommandValidator();
            validator.ShouldNotHaveValidationErrorFor(c => c.Name, command);
        }

       
        [TestCase(-1)]
        [TestCase(0)]
        public void ShouldHaveErrors_Id_lessThenOne(int id)
        {
            var command = CommandFactory.EditProductCommand(id, "Name", "Description", 1, 189m);
            var validator = new EditProductCommandValidator();
            validator.ShouldHaveValidationErrorFor(c => c.Id, command);
        }

        [Test]
        public void ShouldNotHaveErrors_Id_GreaterThenZero()
        {
            var command = CommandFactory.EditProductCommand(1, "Name", "Description", 1, 189m);
            var validator = new EditProductCommandValidator();
            validator.ShouldNotHaveValidationErrorFor(c => c.Id, command);
        }


    }
}
