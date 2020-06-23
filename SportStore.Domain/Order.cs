using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Domain
{
    public class Order : BaseEntity
    {

        public Adress CustomerAdress { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public string Name { get; set; }
        public bool GiftWrap { get; set; }

        public bool Shipped { get; set; }
    }
}
