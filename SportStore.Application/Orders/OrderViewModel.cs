using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application.Orders
{
    public class OrderVm
    {
        public int OrderId { get; set; }
        public bool Shipped { get; set; } = false;        
        public IEnumerable<OrderLineVm> OrderLines { get; set; }
        public CustomerVM Customer { get; set; }
        public bool GiftWrap { get; set; }
    }

    public class OrderLineVm
    {
        public ProductDTO Product { get; set; }
        public int Quantity{ get; set; }
    }
}
