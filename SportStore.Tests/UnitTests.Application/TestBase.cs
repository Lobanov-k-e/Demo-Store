using SportStore.Application;
using SportStore.Application.Interfaces;
using SportStore.Application.Products;
using SportStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Tests.UnitTests.Application
{
    class TestBase
    {
        protected ApplicationContext context;
        protected Mediator mediator;
        protected IMapper mapper = new Mapper();

        public TestBase()
        {
            context = DbContextFactory.Create();
            mediator = new Mediator(context, mapper);
        }

        public void ResetMediatorBindinds()
        {
            
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(context);
        }
    }
}
