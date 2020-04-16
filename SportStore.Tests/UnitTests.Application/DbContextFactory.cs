using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Domain;
using SportStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportStore.Tests.UnitTests.Application
{
    internal class DbContextFactory
    {
        public static ApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);
            context.Database.EnsureCreated();
            SeedData(context);

            return context;
        }

        public static void Destroy(ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void SeedData(ApplicationContext context)
        {
            context.Products.AddRange(Enumerable.Range(0, 10).Select(i => new Product
            {
                Name = $"Product {i}",
                Description = $"Test Product {i}",
                Price = 1000.0M,
                CategoryId = 1,
                Category = new Category { Name = "Category 1", Description = "Test Categoty 1" }
            }));

            context.SaveChanges();
        }
    }
}
