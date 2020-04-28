using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.WebUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICart _cart;

        public OrderController(IMediator mediator, ICart cart)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public IActionResult Checkout()
        {
            return View(new OrderVm());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderVm order)
        {            
            if (_cart.TotalItems == 0)
            {
                ModelState.AddModelError("","cart is empty");
            }
            if (ModelState.IsValid)
            {
                order.OrderLines = _cart.GetOrderLines();
                await _mediator.Handle(new CreateNewOrder() { Order = order });
                return RedirectToAction(nameof(Completed));
            }
            else 
                return View(order);
           
        }

        private IActionResult Completed()
        {
            _cart.Clear();
            return View();

        }
    }
}
