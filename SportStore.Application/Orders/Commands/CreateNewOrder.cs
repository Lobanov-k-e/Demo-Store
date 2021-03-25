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

    public class CreateNewOrderHandler : IRequestHandler<CreateNewOrder, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public CreateNewOrderHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(CreateNewOrder request)
        {
            var order = _mapper.MapOrderVmToDomain(request.Order);
            _context.Products.AttachRange(order.OrderLines.Select(p => p.Product));
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            return order.Id;
        }
    }
}
