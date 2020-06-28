using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Categories.Commands;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Threading.Tasks;
using Moq;
using FluentValidation.TestHelper;
using System.Linq;
using System.Text;

namespace SportStore.UnitTests.UnitTests.Application.CategoriesTests
{
    [TestFixture]
    class EditCategoryTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            CreateNewContext();
        }

        [Test]
        public async Task CanEditCategory()
        {

            var cat = context.Categories.Add(new Domain.Category()).Entity;            
            
            string name = "nameTest";
            string description = "descr";            
            
            var command = CommandFactory.GetEditCategoryCommand(cat.Id, name, description);

            int id = await new EditCategoryHandler(context, mapper).Handle(command);

            var actual = await context.Categories.FindAsync(cat.Id);

            Assert.AreEqual(cat.Id, id);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(description, actual.Description);
        }

        [Test]
        public async Task Throws_On_WrongId()
        {           

            var command = CommandFactory.GetEditCategoryCommand(10000, "name", "description");
            Assert.ThrowsAsync<ArgumentNullException>( async () => await new EditCategoryHandler(context, mapper).Handle(command));

        }

        [Test]
        public void ShouldHaveError_Name_isEmpty()
        {
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "", "testDescr");
            validator.ShouldHaveValidationErrorFor(x => x.Name, command);
        }

        [Test]
        public void ShouldNotHaveError_Name_isSpecified()
        {
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "TestName", "testDescr");
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, command);
        }
        [Test]
        public void ShouldHaveError_Description_isEmpty()
        {
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "TestName", "");
            validator.ShouldHaveValidationErrorFor(x => x.Description, command);
        }

        [Test]
        public void ShouldNotHaveError_Description_isSpecified()
        {
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "TestName", "testDescr");
            validator.ShouldNotHaveValidationErrorFor(x => x.Description, command);
        }

        [Test]
        public void ShouldHaveError_Description_tooLong()
        {
            string s = CreateStringOfLength(400);
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "TestName", s);
            validator.ShouldHaveValidationErrorFor(x => x.Description, command);
        }
        [Test]
        public void ShouldNotHaveError_Description_okLength()
        {
            string s = CreateStringOfLength(200);
            var validator = new EditCategoryValidator();
            var command = CommandFactory.GetEditCategoryCommand(It.IsAny<int>(), "TestName", s);
            validator.ShouldNotHaveValidationErrorFor(x => x.Description, command);
        }

        private static string CreateStringOfLength(int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append("a");
            }

            return sb.ToString();
        }
    }
}
