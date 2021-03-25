using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Commands
{
    public class ShipOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }
    }

    public class ShipOrderRequestHandler : IRequestHandler<ShipOrderCommand, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public ShipOrderRequestHandler(IApplicationContext context, IMapper mapper)        
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(ShipOrderCommand request)
        {
            var order = await _context.Orders.FindAsync(request.OrderId);
            _ = order ?? throw new ArgumentException($"Order with id {request.OrderId} not found");

            order.Shipped = true;
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
