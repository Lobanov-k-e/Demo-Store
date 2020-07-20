using Microsoft.AspNetCore.Mvc.Rendering;
using SportStore.Application.Products.Commands;
using SportStore.Application.Products.Queries;
using System.Collections.Generic;

namespace SportStore.WebUi.ViewModels
{
    public class EditProductViewModel
    {
        public EditProductCommand Command { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList => new CategorySelectListBuilder(Categories).CategorySelectList;
    }
}
