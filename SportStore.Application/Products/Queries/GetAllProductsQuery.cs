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

    public class GetAllProductsQueryRequestHandler : RequestHandlerBase, IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        public GetAllProductsQueryRequestHandler(IApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request)
        {
            return Mapper.MapProductsToDTO(await Context.Products.ToListAsync());
        }
    }
}
