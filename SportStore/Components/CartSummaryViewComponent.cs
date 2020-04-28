using Microsoft.AspNetCore.Mvc;
using SportStore.WebUi.Common;
using SportStore.WebUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICart _cart;

        public CartSummaryViewComponent(ICart cart)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public IViewComponentResult Invoke()
        {
            return View((_cart.TotalItems, _cart.CalculateSumm()));
        }
    }
}
