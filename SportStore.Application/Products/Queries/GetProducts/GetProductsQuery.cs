using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public GetProductsQuery(IApplicationContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(paramName: nameof(context), message: "context should not be null");

            Mapper = new Mapper();
        }

        public IMapper Mapper { get; set; } 

        private readonly IApplicationContext _context;

        
        public async Task<IEnumerable<ProductDTO>> Execute()
        {
            var products = await _context
                .Products
                .Include(p => p.Category)
                .ToListAsync();
            return Mapper.MapProductsToDTO(products);
        }

       
    }
}
