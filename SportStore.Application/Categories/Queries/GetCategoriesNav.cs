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

    public class GetAllCategoriesQueryHandler : RequestHandlerBase, IRequestHandler<GetCategoiesNavQuery, CategoryNavVm>
    {     

        public GetAllCategoriesQueryHandler(IApplicationContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
        public async Task<CategoryNavVm> Handle(GetCategoiesNavQuery request)
        {
            var categories = await Context.Categories.ToListAsync();
            return new CategoryNavVm()
            {
                Categories = Mapper.MapCategoriesToDTO(categories),
                CurrentCategoryName = request.CurrentCategoryName
            };
        }
    }
}
