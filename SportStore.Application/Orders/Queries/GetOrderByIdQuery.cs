using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderVm>
    {
        public int OrderId { get; set; }        
    }


    public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdQuery, OrderVm>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetOrderByIdRequestHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(GetOrderByIdQuery request)
        {
            var order = await _context.Orders.FindAsync(request.OrderId);
            return _mapper.MapOrderToVm(order);
        }
    }
}
