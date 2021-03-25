using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Queries
{
    public class GetOrderDetailsQuery : IRequest<OrderVm>
    {
        public int Id { get; set; }
    }

    public class GetOrderDetailsRequestHandler : IRequestHandler<GetOrderDetailsQuery, OrderVm>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetOrderDetailsRequestHandler(IApplicationContext context, IMapper mapper)            
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderVm> Handle(GetOrderDetailsQuery request)
        {
            var order = await _context
                .Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)               
                .FirstOrDefaultAsync(o => o.Id == request.Id);

            return _mapper.MapOrderToVm(order);
        }
    }
}
