using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.Common;
using SportStore.WebUi.Controllers.ViewModels;
using SportStore.WebUi.ViewModels;
using System;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICart _cart;

        public CartController(IMediator mediator, ICart cart)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartProductVm model)
        {
            var product = await _mediator.Handle(new GetProductByIdQuery() { ProductId = model.ProductId });            
            _cart.AddItem(product, 1);            
            return Redirect(model.ReturnUrl);
        }
        public IActionResult ShowCart(string returnUrl)
        {            
            return View(new CartViewModel(_cart, returnUrl));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var product = await _mediator.Handle(new GetProductByIdQuery() { ProductId = productId });            
            _cart.RemoveItem(product);
            return RedirectToAction("ShowCart");
        }
    }
}
