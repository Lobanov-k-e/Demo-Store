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

        public IActionResult ShowCart(string returnUrl)
        {
            var cart = GetCart();
            return View(new CartViewModel(cart, returnUrl));
        }

        public CartController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartProductVm model)
        {
            var product = await _mediator.Handle(new GetProductByIdQuery() { ProductId = model.ProductId });
            var cart = GetCart();
            cart.AddItem(product, 1);
            SaveCart(cart);
            return Redirect(model.ReturnUrl);
        }


        public async Task<IActionResult> RemoveFromCart(CartProductVm model)
        {
            var product = await _mediator.Handle(new GetProductByIdQuery() { ProductId = model.ProductId });
            var cart = GetCart();
            cart.RemoveItem(product);
            SaveCart(cart);
            return Redirect(model.ReturnUrl);
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
