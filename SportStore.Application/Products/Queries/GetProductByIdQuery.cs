using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdHandler : RequestHandlerBase, IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        public GetProductByIdHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request)
        {
            var product = await Context
                .Products
                .Where(p => p.Id == request.ProductId)
                .SingleOrDefaultAsync();
                
            return Mapper.MapProductToDTO(product);
        }
    }
}
