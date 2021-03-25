using Microsoft.Extensions.DependencyInjection;
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
        public MockMetiator(IServiceProvider serviceProvider) 
            : base(serviceProvider )
        {
        }

        internal void ClearHandlers()
        {
            Handlers.Clear();
        }


    }
}
