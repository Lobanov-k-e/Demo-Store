using SportStore.Application;
using SportStore.Application.Interfaces;
using SportStore.Application.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.UnitTests.UnitTests.Application.MediatorTests
{
    internal class MockMetiator : Mediator
    {
        public MockMetiator(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        internal void ClearHandlers()
        {
            Handlers.Clear();
        }


    }
}
