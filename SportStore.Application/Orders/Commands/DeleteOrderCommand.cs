using FluentValidation;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Commands
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public DeleteOrderRequestHandler(IApplicationContext context, IMapper mapper)            
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(DeleteOrderCommand request)
        {
            var order = await _context.Orders.FindAsync(request.OrderId);
            _ = order ?? throw new ArgumentException(message: $"no order with id {request.OrderId}");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }

    public class DeleteOrderValdator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderValdator()
        {
            RuleFor(c => c.OrderId).GreaterThan(0);
        }
    }
}
