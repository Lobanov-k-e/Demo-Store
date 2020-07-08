using FluentValidation;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Commands
{
    public class EditProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public static EditProductCommand FromProduct(ProductDTO product)
        {
            return new EditProductCommand()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price
            };
        }
    }

    public class EditProductRequestHandler : RequestHandlerBase, IRequestHandler<EditProductCommand, int>
    {
        public EditProductRequestHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<int> Handle(EditProductCommand request)
        {
            var product = await Context.Products.FindAsync(request.Id);
            _ = product ?? 
                throw new ArgumentException(paramName: nameof(request.Id), message:"No product with this id");

            product.Name = request.Name;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            product.Price = request.Price;

            await Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return product.Id;
        }       
    }
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
            RuleFor(c => c.CategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
