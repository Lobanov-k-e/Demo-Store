using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductPageQuery : IQuery<IEnumerable<ProductDTO>>
    {
        private readonly IApplicationContext _context;
        private readonly int _offset;
        private readonly int _take;
        public IMapper Mapper { get; set; }

        public GetProductPageQuery(IApplicationContext context, int pageNumber, int pageSize)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _offset = GetOffset(pageNumber, pageSize);
            _take = pageSize;
            Mapper = new Mapper();
        }
        private static int GetOffset(int pageNumber, int pageSize)
        {
            return (pageNumber > 0 ? pageNumber - 1 : 0) * pageSize;
        }
        public async Task<IEnumerable<ProductDTO>> Execute()
        {
            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip(_offset)
                .Take(_take)
                .Include(p => p.Category)
                .ToListAsync();

            return Mapper.MapProductsToDTO(products);
        }
    }
}
