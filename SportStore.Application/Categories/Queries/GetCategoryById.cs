using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Queries
{
    public class GetCategoryById : IRequest<CategoryDTO>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdHandler : RequestHandlerBase, IRequestHandler<GetCategoryById, CategoryDTO>
    {
        public GetCategoryByIdHandler(IApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CategoryDTO> Handle(GetCategoryById request)
        {
            var category = await Context.Categories.SingleOrDefaultAsync(c => c.Id == request.Id);
            _ = category ?? throw new ArgumentNullException(); // here and everywhere should throw custom categorynotfound exception
            return Mapper.MapCategoryToDTO(category);
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
