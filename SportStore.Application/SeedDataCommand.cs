using Microsoft.Extensions.Primitives;
using SportStore.Application.Interfaces;
using SportStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application
{
    public class SeedDataCommand : IRequest<bool>
    {
    }

    public class SeedDataHandler :  IRequestHandler<SeedDataCommand, bool>
    {
        private readonly IApplicationContext _context;      

        public SeedDataHandler(IApplicationContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));           
        }

        public async Task<bool> Handle(SeedDataCommand request)
        {
            if (!_context.Products.Any())
            {
                List<Category> categories =
                    Enumerable
                    .Range(0, 3)
                    .Select(i => new Category { Name = $"Category {i + 1}", Description = $"Test Categoty {i + 1}" }).ToList();
                var random = new Random();
                _context.Products.AddRange(Enumerable.Range(0, 30).Select((i) => new Product
                {
                    Name = $"Product {i}",
                    Description = $"Test Product {i}",
                    Price = (decimal)random.NextDouble() * 10000,
                    CategoryId = (i / 10) + 1,
                    Category = categories[i / 10]
                }));
                await _context.SaveChangesAsync(new System.Threading.CancellationToken());
                return true;
            }
            return false;
        }
    }
}
