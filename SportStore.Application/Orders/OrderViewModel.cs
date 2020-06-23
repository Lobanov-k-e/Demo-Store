using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application.Orders
{
    public class OrderVm
    {
        public bool Shipped { get; set; } = false;
        public int OrderId { get; set; }
        public IEnumerable<OrderLineVm> OrderLines { get; set; }
        public CustomerVM Customer { get; set; }
        public bool GiftWrap { get; set; }
    }

    public class OrderLineVm
    {
        public ProductDTO Product { get; set; }
        public int Quantity{ get; set; }
    }

    public class CustomerVM
    {
        public string Name { get; set; }
        public AdressVm Adress { get; set; }
    }

    public class AdressVm
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}
