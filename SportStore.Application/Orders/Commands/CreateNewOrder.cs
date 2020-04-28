using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Commands
{
    public class CreateNewOrder : IRequest<int>
    {
        public OrderVm Order { get; set; }
    }

    public class CreateNewOrderHandler : RequestHandlerBase, IRequestHandler<CreateNewOrder, int>
    {
        public CreateNewOrderHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<int> Handle(CreateNewOrder request)
        {
            var order = Mapper.MapOrderVmToDomain(request.Order);
            Context.Products.AttachRange(order.OrderLines.Select(p => p.Product));
            Context.Orders.Add(order);
            await Context.SaveChangesAsync(new System.Threading.CancellationToken());
            return order.Id;
        }
    }
}
