using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Application.Categories.Queries;
using SportStore.Tests.UnitTests.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.Application.CategoriesTests
{
    [TestFixture]
    class GetCategoryByIdTests : TestBase
    {
        [Test]
        public async Task CanGetCategory()
        {
            var category = await context.Categories.FirstAsync();
            var query = QueryFactory.GetCategoryByIdQuery(category.Id);

            var result = await new GetCategoryByIdHandler(context, mapper).Handle(query);

            Assert.AreEqual(category.Id, result.Id);
        }

        [Test]
        public async Task Throws_CategoryNotFound()
        {
            var query = QueryFactory.GetCategoryByIdQuery(10000);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await new GetCategoryByIdHandler(context, mapper).Handle(query));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ShouldHaveErrors_idLessThenOne(int id)
        {
            var query = QueryFactory.GetCategoryByIdQuery(id);
            var validator = new GetCategoryByIdValidator();
            validator.ShouldHaveValidationErrorFor(c => c.Id, query);
        }
        [Test]
        public void ShouldNotHaveErrors_idGreaterThenZero()
        {
            var query = QueryFactory.GetCategoryByIdQuery(1);
            var validator = new GetCategoryByIdValidator();
            validator.ShouldNotHaveValidationErrorFor(c => c.Id, query);
        }
    }
}
