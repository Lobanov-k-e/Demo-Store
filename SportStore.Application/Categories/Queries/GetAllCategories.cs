using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Queries
{
    public class GetAllCategories : IRequest<IEnumerable<CategoryDTO>>
    {

    }

    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, IEnumerable<CategoryDTO>>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategories request)
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(c => _mapper.MapCategoryToDTO(c));
        }
    }
}
