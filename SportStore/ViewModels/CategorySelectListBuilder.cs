using Microsoft.AspNetCore.Mvc.Rendering;
using SportStore.Application.Products.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.WebUi.ViewModels
{
    internal class CategorySelectListBuilder
    {
        private readonly IEnumerable<CategoryDTO> Categories;
        public CategorySelectListBuilder(IEnumerable<CategoryDTO> categories)
        {
            Categories = categories;
        }

        public IEnumerable<SelectListItem> CategorySelectList => Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));
    }
}
