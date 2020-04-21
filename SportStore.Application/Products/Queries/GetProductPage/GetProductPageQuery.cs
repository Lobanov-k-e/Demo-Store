using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductPageQuery : IRequest<ProductPageVM>
    {   //validation needed
        public GetProductPageQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;           
        }
        public int PageNumber { get; }
        public int PageSize { get; }
    }

    public class GetProductPageQueryHandler : IRequestHandler<GetProductPageQuery, ProductPageVM>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetProductPageQueryHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(paramName: nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
        }

        public async Task<ProductPageVM> Handle(GetProductPageQuery request)
        {

            int offset = GetOffset(request);

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip(offset)
                .Take(request.PageSize)
                .Include(p => p.Category)
                .ToListAsync();

            int productsCount = await _context.Products.CountAsync();

            var Vm = new ProductPageVM()
            {
                Products = _mapper.MapProductsToDTO(products),
                PageInfo = new PageInfo()
                {
                    ItemsCount = productsCount,
                    CurrentPage = request.PageNumber, //проблема - отображаемая страница при < 0
                    ItemsPerPage  = request.PageSize
                }
            };
            return Vm;            
        }

        private static int GetOffset(GetProductPageQuery request)
        {
            return (request.PageNumber > 0 ? request.PageNumber - 1 : 0) * request.PageSize;
        }
       
    }
}
