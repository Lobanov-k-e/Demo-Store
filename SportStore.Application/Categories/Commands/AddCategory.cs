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

    public class AddCategoryRequestHandler : RequestHandlerBase, IRequestHandler<AddCategoryCommand, int>
    {
        public AddCategoryRequestHandler(IApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> Handle(AddCategoryCommand request)
        {
            Category category = Mapper.MapCategoryToDomain(
                new CategoryDTO() 
                { 
                    Name = request.Name, 
                    Description = request.Description
                });
            Context.Categories.Add(category);
            await Context.SaveChangesAsync(new System.Threading.CancellationToken());
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
