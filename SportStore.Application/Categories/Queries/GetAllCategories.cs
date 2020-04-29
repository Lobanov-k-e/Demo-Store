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

    public class GetAllCategoriesHandler : RequestHandlerBase, IRequestHandler<GetAllCategories, IEnumerable<CategoryDTO>>
    {
        public GetAllCategoriesHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategories request)
        {
            var categories = await Context.Categories.ToListAsync();
            return categories.Select(c => Mapper.MapCategoryToDTO(c));
        }
    }
}
