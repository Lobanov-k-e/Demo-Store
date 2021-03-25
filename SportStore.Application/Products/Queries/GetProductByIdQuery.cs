using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IApplicationContext context, IMapper mapper)            
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request)
        {
            var product = await _context
                .Products
                .Where(p => p.Id == request.ProductId)
                .SingleOrDefaultAsync();
                
            return _mapper.MapProductToDTO(product);
        }
    }
}
