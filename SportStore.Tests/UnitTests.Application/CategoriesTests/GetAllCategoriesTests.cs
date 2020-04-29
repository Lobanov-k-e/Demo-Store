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
    class GetAllCategoriesTests : TestBase
    {
        [Test]
        public async Task CanGetCategories()
        {
            var query = queryFactory.GetAllCategoriesQuery();
            var handler = new GetAllCategoriesHandler(context, mapper);

            var result = await handler.Handle(query);

            int expected = context.Categories.Count();

            Assert.AreEqual(expected, result.Count());
        }

    }
}
