using NUnit.Framework;
using SportStore.Application.Categories.Commands;
using SportStore.Tests.UnitTests.Application;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace SportStore.UnitTests.UnitTests.Application.CategoriesTests
{
    [TestFixture]
    class AddCategoryTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var categories = context.Categories.ToList();
            context.Categories.RemoveRange(categories);
            context.SaveChanges();
        }
        [Test]
        public async Task CanAddCategory()
        {
            var command = CommandFacory.GetAddCategoryCommand("TestName","TestDescription");

            int categoryId = await new AddCategoryRequestHandler(context, mapper).Handle(command);
            var category = await context.Categories.FindAsync(categoryId);

            Assert.IsNotNull(category);
            Assert.AreEqual(command.Name, category.Name);
            Assert.AreEqual(command.Description, category.Description);            
        }

        [Test]
        public void ShouldHaveErrors_Name_isEmpty()
        {
            var validator = new AddCategoryValidator();
            var command = CommandFacory.GetAddCategoryCommand("", "TestDescr");
            validator.ShouldHaveValidationErrorFor(x => x.Name, command);
        }
        [Test]
        public void ShouldNotHaveErrors_Name_isSpecified()
        {
            var validator = new AddCategoryValidator();
            var command = CommandFacory.GetAddCategoryCommand("Name", "TestDescr");
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, command);
        }

        [Test]
        public void ShouldHaveErrors_Description_isEmpty()
        {
            var validator = new AddCategoryValidator();
            var command = CommandFacory.GetAddCategoryCommand("Name", "");
            validator.ShouldHaveValidationErrorFor(x => x.Description, command);
        }
        [Test]
        public void ShouldNotHaveErrors_Description_isSpecified()
        {
            var validator = new AddCategoryValidator();
            var command = CommandFacory.GetAddCategoryCommand("Name", "TestDescr");
            validator.ShouldNotHaveValidationErrorFor(x => x.Description, command);
        }
    }
}
