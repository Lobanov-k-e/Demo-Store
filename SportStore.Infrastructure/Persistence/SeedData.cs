﻿
using SportStore.Domain;
using SportStore.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Infrastructure
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationContext context)
        {
            _ = context ??
                 throw new ArgumentNullException(paramName: nameof(context), message: "Context should not be null");

            if (!context.Products.Any())  
            {
               
                context.Products.AddRange(Enumerable.Range(0, 30).Select((i) => new Product
                {                    
                    Name = $"Product {i}",
                    Description = $"Test Product {i}",
                    Price = 1000.0M,
                    CategoryId = (i / 10) + 1,
                    Category = new Category { Name = $"Category {(i / 10) + 1}", Description = $"Test Categoty {(i / 10) + 1}" }
                })); 
                await context.SaveChangesAsync();
            }
        }

               
       
    }
}
