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
        protected IMapper mapper = new Mapper();
        protected QueryFactory queryFactory = new QueryFactory();

        public TestBase()
        {
            context = DbContextFactory.Create();            
        }
        public TestBase(bool asNoTracking)
        {
            context = DbContextFactory.Create(asNoTracking);            
        }       

        public void CreateNewContext(bool asNoTracking = false)
        {
            context = DbContextFactory.Create(asNoTracking);
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(context);
        }
    }
}
