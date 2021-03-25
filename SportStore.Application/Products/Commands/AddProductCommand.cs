using FluentValidation;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public AddProductCommand()
        {

        }
        public AddProductCommand(ProductDTO product)
        {
            _ = product ?? 
                    throw new ArgumentNullException(message: "productDTO should not pe null", paramName: nameof(product)); 

            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            CategoryId = product.CategoryId;
        }

        public ProductDTO GetProductDTO()
        {
            return new ProductDTO
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                CategoryId = this.CategoryId
            };
        }
    }

    public class AddProductRequestHandler :  IRequestHandler<AddProductCommand, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public AddProductRequestHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(AddProductCommand request)
        {
            var product = _mapper.MapProductToDomain(request.GetProductDTO());
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            return product.Id;           
        }
    }

    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
            RuleFor(c => c.CategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
