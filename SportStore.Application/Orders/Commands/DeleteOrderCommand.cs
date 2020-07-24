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

    public class DeleteOrderRequestHandler : RequestHandlerBase, IRequestHandler<DeleteOrderCommand, int>
    {
        public DeleteOrderRequestHandler(IApplicationContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<int> Handle(DeleteOrderCommand request)
        {
            var order = await Context.Orders.FindAsync(request.OrderId);
            _ = order ?? throw new ArgumentException(message: $"no order with id {request.OrderId}");

            Context.Orders.Remove(order);
            await Context.SaveChangesAsync(new System.Threading.CancellationToken());
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
