using FluentValidation;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Commands
{
    public class EditCategory : IRequest<int>
    {
        public EditCategory()
        {

        }

        public EditCategory(CategoryDTO categoryDTO)
        {
            Id = categoryDTO.Id;
            Name = categoryDTO.Name;
            Description = categoryDTO.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditCategoryHandler : RequestHandlerBase, IRequestHandler<EditCategory, int>
    {
        public EditCategoryHandler(IApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> Handle(EditCategory request)
        {
            var category = await Context.Categories.FindAsync(request.Id);
            _ = category ?? throw new ArgumentNullException();
            category.Name = request.Name;
            category.Description = request.Description;
            await Context.SaveChangesAsync(new System.Threading.CancellationToken());
            return category.Id;
        }
    }

    class EditCategoryValidator : AbstractValidator<EditCategory>
    {
        public EditCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty().MaximumLength(300);
        }
    }
}
