using SportStore.Application.Products.Queries;
using SportStore.WebUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.ViewModels
{
    public class CartViewModel
    {
        public string ReturnUrl { get; }
        public IEnumerable<CartLine> Lines { get;}

        public decimal CartTotal { get;}

        public CartViewModel(ICart cart, string returnUrl)
        {
            _ = cart ?? throw new ArgumentNullException(paramName: nameof(cart));

            Lines = cart.GetLines();
            CartTotal = cart.CalculateSumm();
            ReturnUrl = returnUrl;
        }
    }
}
