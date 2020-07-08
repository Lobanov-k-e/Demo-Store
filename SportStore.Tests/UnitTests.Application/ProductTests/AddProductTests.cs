using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Categories.Commands;
using SportStore.Application.Products.Commands;
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
    class AddProductTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }

        [Test]
        public async Task CanAddProduct()
        {
            const int CatIt = 1;
            const int Price = 10;
            const string Name = "name";
            const string Description = "descr";
            var command = CommandFactory.AddProduct(Name, Description, CatIt, Price);            
            int productId = await new AddProductRequestHandler(context, mapper).Handle(command);

            var actual = await context.Products.SingleOrDefaultAsync(p=>p.Id == productId);

            Assert.NotNull(actual);
            Assert.AreEqual(CatIt, actual.CategoryId);
            Assert.AreEqual(Price, actual.Price);
            Assert.AreEqual(Name, actual.Name);
            Assert.AreEqual(Description, actual.Description);
        }

        [Test]
        public void CtorThrows_ProductDTO_isNull()
        {
            ArgumentNullException exception = null;
            try
            {
                new AddProductCommand(null);
            }
            catch (ArgumentNullException e)
            {
                exception = e;                
            }

            Assert.NotNull(exception);
            Assert.IsTrue(exception.Message.Contains("productDTO should not pe null"));
            Assert.AreEqual(exception.ParamName, "product");
            
        }

        [Test]
        public void ShouldHaveErrors_name_isEmpty()
        {
            var command = CommandFactory.AddProduct(" ", "descr", 1, 12);
            var validator = new AddProductCommandValidator();
            validator.ShouldHaveValidationErrorFor(c => c.Name, command);
        }
        [Test]
        public void ShouldNotHaveErrors_name_isSet()
        {
            var command = CommandFactory.AddProduct("Name", "descr", 1, 12);
            var validator = new AddProductCommandValidator();
            validator.ShouldNotHaveValidationErrorFor(c => c.Name, command);
        }

    }
}
