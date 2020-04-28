using FluentValidation;

namespace SportStore.Application.Orders
{
    class OrderVmValidation : AbstractValidator<OrderVm>
    {
        public OrderVmValidation()
        {
            RuleFor(m => m.Customer.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(m=>m.Customer.Adress.Line1).NotEmpty().WithMessage("Address is required");
            RuleFor(m=>m.Customer.Adress.Country).NotEmpty().WithMessage("Country is required");              
        }
    }
}
