using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderListItemDTO>>
    {

    }

    public class GetAllOrdersRequestHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderListItemDTO>>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllOrdersRequestHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderListItemDTO>> Handle(GetAllOrdersQuery request)
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.MapOrdersToDTO(orders);
        }
    }
}
