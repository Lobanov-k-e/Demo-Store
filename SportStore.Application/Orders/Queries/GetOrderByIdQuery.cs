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


    public class GetOrderByIdRequestHandler : RequestHandlerBase, IRequestHandler<GetOrderByIdQuery, OrderVm>
    {
        public GetOrderByIdRequestHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<OrderVm> Handle(GetOrderByIdQuery request)
        {
            var order = await Context.Orders.FindAsync(request.OrderId);
            return Mapper.MapOrderToVm(order);
        }
    }
}
