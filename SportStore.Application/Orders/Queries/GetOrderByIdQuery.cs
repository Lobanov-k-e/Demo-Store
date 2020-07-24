using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderVm>
    {
        public int OrderId { get; set; }        
    }

    


}
