using FluentValidation.TestHelper;
using NUnit.Framework;
using SportStore.Application.Categories.Commands;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.CategoriesTests
{
    [TestFixture]
    class DeleteCategoryTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }
        [Test]
        public async Task CanDeleteCategory()
        {
            int categoriesCount = context.Categories.Count();

            int id = context.Categories.Add(new Domain.Category()).Entity.Id;
            await context.SaveChangesAsync();

            var command = CommandFactory.GetDeleteCategoryCommand(id);
            await new DeleteCategoryRequestHandler(context, mapper).Handle(command);

            Assert.AreEqual(categoriesCount, context.Categories.Count());
        }
        [Test]
        public async Task CanDeleteCorrectCategory()
        {
            int id = context.Categories.Add(new Domain.Category()).Entity.Id;
            await context.SaveChangesAsync();

            var command = CommandFactory.GetDeleteCategoryCommand(id);
            await new DeleteCategoryRequestHandler(context, mapper).Handle(command);

            Assert.IsNull(context.Categories.Where(c => c.Id == id).SingleOrDefault());
        }

        [Test]
        public async Task Throws_categoryNotFound()
        {
            var command = CommandFactory.GetDeleteCategoryCommand(10000);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await new DeleteCategoryRequestHandler(context, mapper).Handle(command));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ShouldHaveErrors_Id_lessThanOne(int i)
        {
            var command = CommandFactory.GetDeleteCategoryCommand(i);
            var validator = new DeleteCategoryValidator();
            validator.ShouldHaveValidationErrorFor(c => c.Id, command);
        }
        [Test]
        public void ShouldNotHaveErrors_Id_greaterThanZero()
        {
            var command = CommandFactory.GetDeleteCategoryCommand(1);
            var validator = new DeleteCategoryValidator();
            validator.ShouldNotHaveValidationErrorFor(c => c.Id, command);
        }


    }
}
