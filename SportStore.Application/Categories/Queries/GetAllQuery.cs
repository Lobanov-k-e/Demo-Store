using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<CategoryListVM>
    {
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, CategoryListVM>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CategoryListVM> Handle(GetAllCategoriesQuery request)
        {
            var categories = await _context.Categories.ToListAsync();
            return new CategoryListVM()
            {
                Categories = _mapper.MapCategoriesToDTO(categories)
            };
        }
    }
}
