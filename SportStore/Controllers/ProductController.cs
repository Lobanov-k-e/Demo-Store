using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Categories.Queries;
using SportStore.Application.Products.Commands;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.Common;
using SportStore.WebUi.ViewModels;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class ProductController : ControllerBase
    {
        private const string PRODUCTLIST_ACTION = nameof(ProductList);

        public ProductController(IMediator mediator) : base(mediator)
        {

        }

        public async Task<ActionResult> Products(string currentCategory, int pageNumber = 1)
        {
            int pageSize = 3;
            var result = await Mediator.Handle(new GetProductPageQuery(pageNumber, pageSize, currentCategory));
            return View(result);
        }

        public async Task<IActionResult> ProductList()
        {
            return View(await Mediator.Handle(new GetAllProductsQuery()));
        }

        public async Task<IActionResult> AddProduct()
        {            
            var categories = await Mediator.Handle(new GetAllCategories());
            var model = new AddProductViewModel() { Categories = categories};            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Handle(command);
                return RedirectToAction(PRODUCTLIST_ACTION);
            }
            var categories = await Mediator.Handle(new GetAllCategories());

            return View(new AddProductViewModel() { Command = command, Categories = categories});
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await Mediator.Handle(new GetProductByIdQuery() { ProductId = id });            
            if (product is null)
            {
                return NotFound();
            }
            var categories = await Mediator.Handle(new GetAllCategories());
            var model = new EditProductViewModel() { Command = EditProductCommand.FromProduct(product), Categories = categories };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Handle(command);
                return RedirectToAction(PRODUCTLIST_ACTION);
            }
            var categories = await Mediator.Handle(new GetAllCategories());

            return View(new EditProductViewModel() { Command = command, Categories = categories});
        }
              

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand command)
        {
            await Mediator.Handle(command);
            return RedirectToAction(PRODUCTLIST_ACTION);
        }
    }
}