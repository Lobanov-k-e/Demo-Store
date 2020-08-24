using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Queries
{
    public class GetOrderDetailsQuery : IRequest<OrderVm>
    {
        public int Id { get; set; }
    }

    public class GetOrderDetailsRequestHandler : RequestHandlerBase,
        IRequestHandler<GetOrderDetailsQuery, OrderVm>
    {
        public GetOrderDetailsRequestHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<OrderVm> Handle(GetOrderDetailsQuery request)
        {
            var order = await Context
                .Orders
                .Include(o => o.OrderLines)                
                .FirstOrDefaultAsync(o => o.Id == request.Id);
            return Mapper.MapOrderToVm(order);
        }
    }
}
