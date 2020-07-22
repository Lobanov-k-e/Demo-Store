using FluentValidation;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductRequestHandler : RequestHandlerBase, IRequestHandler<DeleteProductCommand, int>
    {
        public DeleteProductRequestHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<int> Handle(DeleteProductCommand request)
        {
            var product = await Context.Products.FindAsync(request.ProductId);
            _ = product ?? throw new ArgumentException(message: $"Product with id {request.ProductId}  not found");

            Context.Products.Remove(product);
            await Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return product.Id;
        }
    }

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(c => c.ProductId).NotEmpty().GreaterThan(0);
        }
    }
}
