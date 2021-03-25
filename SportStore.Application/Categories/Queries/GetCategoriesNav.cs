using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Queries
{
    public class GetCategoiesNavQuery : IRequest<CategoryNavVm>
    {
        public string CurrentCategoryName { get; set; }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetCategoiesNavQuery, CategoryNavVm>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CategoryNavVm> Handle(GetCategoiesNavQuery request)
        {
            var categories = await _context.Categories.ToListAsync();
            return new CategoryNavVm()
            {
                Categories = _mapper.MapCategoriesToDTO(categories),
                CurrentCategoryName = request.CurrentCategoryName
            };
        }
    }
}
