using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }

    public class GetAllProductsQueryRequestHandler :  IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryRequestHandler(IApplicationContext context, IMapper mapper) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request)
        {
            return _mapper.MapProductsToDTO(await _context.Products.ToListAsync());
        }
    }
}
