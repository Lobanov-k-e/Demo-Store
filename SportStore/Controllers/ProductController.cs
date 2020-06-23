using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.Common;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class ProductController : ControllerBase  
    {       

        public ProductController(IMediator mediator) : base(mediator)
        {
            
        }

        public async Task<ActionResult> ProductList(string currentCategory, int pageNumber = 1)
        {
            int pageSize = 3;
            var result = await Mediator.Handle(new GetProductPageQuery(pageNumber, pageSize, currentCategory));            
            return View(result);
        }
    }
}