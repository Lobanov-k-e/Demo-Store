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

    public class EditCategoryHandler : IRequestHandler<EditCategory, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public EditCategoryHandler(IApplicationContext context, IMapper mapper) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(EditCategory request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            _ = category ?? throw new ArgumentException(message: "No category with this id", nameof(request.Id));
            category.Name = request.Name;
            category.Description = request.Description;
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            return category.Id;
        }
    }

    public class EditCategoryValidator : AbstractValidator<EditCategory>
    {
        public EditCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Description).NotEmpty().MaximumLength(300);
        }
    }
}
