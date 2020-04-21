using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Products.Queries;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> ProductList(int pageNumber = 1)
        {
            int pageSize = 3;
            var result = await _mediator.Handle(new GetProductPageQuery(pageNumber, pageSize));
            return View(result);
        }
    }
}