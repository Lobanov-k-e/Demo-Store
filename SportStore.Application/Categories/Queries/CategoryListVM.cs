using SportStore.Application.Products.Queries;
using System.Collections.Generic;

namespace SportStore.Application.Categories.Queries
{
    public class CategoryListVM
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}