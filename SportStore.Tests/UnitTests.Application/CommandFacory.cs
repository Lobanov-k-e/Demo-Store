using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.UnitTests.UnitTests.Application
{
    class CommandFacory
    {
        public static CreateNewOrder GetCreateNewOrderCommand(OrderVm order)
        {
            return new CreateNewOrder() { Order = order};
        }
    }
}
