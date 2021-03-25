using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Queries
{
    public class GetCategoryById : IRequest<CategoryDTO>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdHandler :  IRequestHandler<GetCategoryById, CategoryDTO>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryDTO> Handle(GetCategoryById request)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id);
            _ = category ?? throw new ArgumentNullException(); // here and everywhere should throw custom categorynotfound exception
            return _mapper.MapCategoryToDTO(category);
        }

    }

    public class GetCategoryByIdValidator : AbstractValidator<GetCategoryById>
    {
        public GetCategoryByIdValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
