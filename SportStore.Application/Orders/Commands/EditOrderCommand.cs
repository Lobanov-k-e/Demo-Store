using FluentValidation;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Orders.Commands
{
    public class EditOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }      
        public CustomerVM Customer { get; set; }
        public bool IsGiftWrap { get; set; }
        public bool Shipped { get; set; }

        public static EditOrderCommand FromOrder(OrderVm order)
        {
            return new EditOrderCommand()
            {
                OrderId = order.OrderId,
                Customer = order.Customer,
                IsGiftWrap = order.GiftWrap,
                Shipped = order.Shipped
            };
        }
    }

    public class EditOrderRequstHandler : RequestHandlerBase, IRequestHandler<EditOrderCommand, int>
    {
        public EditOrderRequstHandler(IApplicationContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<int> Handle(EditOrderCommand request)
        {
            var order = await Context.Orders.FindAsync(request.OrderId);
            _ = order ?? throw new ArgumentException($"Order with id {request.OrderId} not found");

            order.Name = request.Customer.Name;
            order.GiftWrap = request.IsGiftWrap;
            order.CustomerAdress = Mapper.MapAdressToDomain(request.Customer.Adress);
            order.Shipped = request.Shipped;

            await Context.SaveChangesAsync(new System.Threading.CancellationToken());
            return order.Id;
        }
        
    }
    public class EditOrderValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderValidator()
        {
            //throws if adress or customer is null
            RuleFor(c => c.Customer.Adress.City).NotEmpty();
            RuleFor(c => c.Customer.Adress.Country).NotEmpty().WithMessage("Country should not be empty");
            RuleFor(c => c.Customer.Adress.Line1).NotEmpty();
            RuleFor(c => c.Customer.Adress.Zip).NotEmpty();
            RuleFor(c => c.Customer.Name).NotEmpty();
        }
    }
}
