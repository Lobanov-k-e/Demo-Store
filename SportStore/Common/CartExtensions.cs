using SportStore.Application.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.Common
{
    public static class CartExtensions
    {
        //cart line is not needed, should use orderlinevm inside cart already        
        public static IEnumerable<OrderLineVm> GetOrderLines(this ICart cart)
        {
            return cart.Lines.Select(l => new OrderLineVm() { Product = l.Product, Quantity = l.Quantity });
        }
    }
}
