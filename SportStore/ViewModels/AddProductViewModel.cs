using Microsoft.AspNetCore.Mvc.Rendering;
using SportStore.Application;
using SportStore.Application.Products.Commands;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.ViewModels
{
    public class AddProductViewModel
    {       
        public AddProductCommand Command { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }

        public IEnumerable<SelectListItem> GetCategorySelectList()
        {
            return Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString()));            
        }
    }
}
