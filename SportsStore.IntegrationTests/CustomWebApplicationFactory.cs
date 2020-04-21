using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Domain;
using SportStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SportsStore.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var contextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                if (contextDescriptor != null)
                {
                    services.Remove(contextDescriptor);
                }
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationContext>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        SeedSampleData(context);
                    }
                    catch (Exception ex)
                    {
                        // log error
                    }
                }
            }).UseEnvironment("Test");
        }
        private void SeedSampleData(ApplicationContext context)
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
        public HttpClient GetClient()
        {
            return CreateClient();
        }
    }    
}
