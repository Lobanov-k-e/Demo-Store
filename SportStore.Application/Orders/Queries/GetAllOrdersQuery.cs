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

    public class GetAllOrdersRequestHandler : RequestHandlerBase, IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderListItemDTO>>
    {
        public GetAllOrdersRequestHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<OrderListItemDTO>> Handle(GetAllOrdersQuery request)
        {
            var orders = await Context.Orders.ToListAsync();
            return Mapper.MapOrdersToDTO(orders);
        }
    }
}
