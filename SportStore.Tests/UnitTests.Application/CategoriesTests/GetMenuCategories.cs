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
    class GetCategoriesNavTest : TestBase
    {
        [Test]
        public async Task Returns_allCategories()
        {
            var act = await new GetAllCategoriesQueryHandler(context, mapper).Handle(new GetCategoiesNavQuery());
            var expected = context.Categories.Count();
            Assert.AreEqual(act.Categories.Count(), expected);        
        }

        [Test]
        public async Task Returns_CorrectCategories()
        {
            var act = await new GetAllCategoriesQueryHandler(context, mapper).Handle(new GetCategoiesNavQuery());
            var expected = mapper.MapCategoriesToDTO(await context.Categories.ToListAsync());
            CollectionAssert.AreEqual(expected, act.Categories, new CategoryDTOByIdComparer());          
        }
    }
}
