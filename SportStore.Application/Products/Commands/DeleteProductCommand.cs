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

    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IApplicationContext _context;
      

        public DeleteProductRequestHandler(IApplicationContext context)             
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }

        public async Task<int> Handle(DeleteProductCommand request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            _ = product ?? throw new ArgumentException(message: $"Product with id {request.ProductId}  not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

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
