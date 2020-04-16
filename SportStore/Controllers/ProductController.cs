using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Products.Queries;

namespace SportStore.WebUi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductQueryFactory productQueryFactory;

        public ProductController(IProductQueryFactory productQueryFactory)
        {
            this.productQueryFactory = productQueryFactory 
                ?? throw new ArgumentNullException(paramName: nameof(productQueryFactory), 
                message: "ProductQueryFactory should not be null");
        }
        public async Task<IActionResult> ProductList(int pageNumber = 1)
        {
            const int DefaultPageSize = 4;
            var query = productQueryFactory.GetProductPageQuery(pageNumber, DefaultPageSize);
            return View(await query.Execute());
        }
    }
}