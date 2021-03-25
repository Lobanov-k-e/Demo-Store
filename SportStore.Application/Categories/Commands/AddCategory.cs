using FluentValidation;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using SportStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Commands
{
    public class AddCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddCategoryRequestHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public AddCategoryRequestHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(AddCategoryCommand request)
        {
            Category category = _mapper.MapCategoryToDomain(
                new CategoryDTO() 
                { 
                    Name = request.Name, 
                    Description = request.Description
                });
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            return category.Id;
        }        
    }
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty().MaximumLength(300);
        }
    }
}
