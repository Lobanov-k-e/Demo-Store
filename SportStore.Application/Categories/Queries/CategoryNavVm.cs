using SportStore.Application.Products.Queries;
using System.Collections.Generic;

namespace SportStore.Application.Categories.Queries
{
    public class CategoryNavVm
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public string CurrentCategoryName { get; set; }
    }
}