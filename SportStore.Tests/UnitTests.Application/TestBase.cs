using SportStore.Application;
using SportStore.Application.Interfaces;
using SportStore.Application.Products;
using SportStore.Infrastructure.Persistence;
using SportStore.UnitTests.UnitTests.Application;
using SportStore.UnitTests.UnitTests.Application.MediatorTests;

namespace SportStore.Tests.UnitTests.Application
{
    class TestBase
    {
        protected ApplicationContext context;
        protected Mediator mediator;
        protected IMapper mapper = new Mapper();
        protected QueryFactory queryFactory = new QueryFactory();

        public TestBase()
        {
            context = DbContextFactory.Create();
            mediator = new MockMetiator(context, mapper);
        }

        public void ResetMediatorBindinds()
        {
            (mediator as MockMetiator).ClearHandlers();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(context);
        }
    }
}
