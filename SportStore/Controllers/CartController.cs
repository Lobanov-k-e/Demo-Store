using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.Common;
using SportStore.WebUi.Controllers.ViewModels;
using System;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartVm model)
        {
            var product = await _mediator.Handle(new GetProductByIdQuery() { ProductId = model.ProductId });
            var cart = GetCart();
            cart.AddItem(product, 1);
            SaveCart(cart);
            return RedirectToAction("Index", model.ReturnUrl);
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }
    }
}
