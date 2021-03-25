using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Application.Orders.Queries;
using SportStore.WebUi.Common;
using System;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class OrderController : ControllerBase
    {
        
        private readonly ICart _cart;

        public OrderController(IMediator mediator, ICart cart) : base(mediator)
        {           
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
                await Mediator.Handle(new CreateNewOrder() { Order = order });
                return RedirectToAction(nameof(Completed));
            }
            else 
                return View(order);           
        }

        public IActionResult Completed()
        {
            _cart.Clear();
            return View();
        }

        [Authorize]
        public async Task<IActionResult> OrderList()
        {
            return View(await Mediator.Handle(new GetAllOrdersQuery()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(DeleteOrderCommand command)
        {
            await Mediator.Handle(command);
            return RedirectToAction(nameof(OrderList));
        }
        [Authorize]
        public async Task<IActionResult> EditOrder(int orderId)
        {
            var order = await Mediator.Handle(new GetOrderByIdQuery() { OrderId = orderId });
            if (order is null)
            {
                return NotFound();
            }

            return View(EditOrderCommand.FromOrder(order)); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditOrder(EditOrderCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Handle(command);
                return RedirectToAction(nameof(OrderList));
            }
            return View(command);
        }
        [Authorize]
        public async Task<IActionResult> Details(GetOrderDetailsQuery query)
        {
            var order = await Mediator.Handle(query);
            return View(order); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ShipOrder(ShipOrderCommand command)
        {
            await Mediator.Handle(command);
            return RedirectToAction(nameof(OrderList));
        }
    }
}
