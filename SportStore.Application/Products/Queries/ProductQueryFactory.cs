using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application.Products.Queries
{
    public class ProductQueryFactory : IProductQueryFactory
    {
        private readonly IApplicationContext _context;

        public ProductQueryFactory(IApplicationContext context)
        {
            _context = context ??
                throw new ArgumentNullException(paramName: nameof(context), message: "context should not be null"); ;
        }
        public IQuery<IEnumerable<ProductDTO>> GetAllQuery()
        {
            return new GetProductsQuery(_context);
        }

        public IQuery<IEnumerable<ProductDTO>> GetProductPageQuery(int pageNumber, int pageSize)
        {
            return new GetProductPageQuery(_context, pageNumber, pageSize);
        }
    }   
}
