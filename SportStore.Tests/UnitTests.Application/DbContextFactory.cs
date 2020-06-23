using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportStore.Domain;
using SportStore.Infrastructure.Persistence;
using System;
using System.Linq;

namespace SportStore.Tests.UnitTests.Application
{
    internal class DbContextFactory
    {
      
        public static ApplicationContext Create(bool asNoTracking = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            if (asNoTracking)
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
                

            var context = new ApplicationContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            SeedData(context);
            if (asNoTracking)
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }
            }

            return context;
        }

        public static void Destroy(ApplicationContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void SeedData(ApplicationContext context)
        { 
            Category category = new Category { Name = "Category 1", Description = "Test Categoty 1" };
            context.Products.AddRange(Enumerable.Range(0, 10).Select(i =>
            {               
                return new Product
                {
                    Name = $"Product {i}",
                    Description = $"Test Product {i}",
                    Price = 1000.0M,
                    CategoryId = 1,
                    Category = category
                };
            }));
            category = new Category { Name = "Category 2", Description = "Test Categoty 2" };
            context.Products.AddRange(Enumerable.Range(0, 10).Select(i => new Product
            {
                Name = $"Product {i}",
                Description = $"Test Product {i}",
                Price = 1000.0M,
                CategoryId = 1,
                Category = category
            }));
           
            context.SaveChanges();
        }
    }
}
