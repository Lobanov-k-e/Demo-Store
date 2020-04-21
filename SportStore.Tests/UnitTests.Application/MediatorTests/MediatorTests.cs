using NUnit.Framework;
using SportStore.Application;
using SportStore.Application.Products.Queries;
using System.Threading.Tasks;

namespace SportStore.Tests.UnitTests.Application.MediatorTests
{
    [TestFixture]
    class MediatorTests : TestBase
    {

        [TearDown]
        public void TearDown()
        {
            ResetMediatorBindinds();
        }

        [Test]
        public async Task MediatorWorks()
        {           

            var mediator = new Mediator(context, mapper);
            Mediator.Register<GetProductPageQuery, GetProductPageQueryHandler>();

            var query = new GetProductPageQuery(1, 4);

            var result = await mediator.Handle(query);
            var expected = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            Assert.AreEqual(expected.GetType(), result.GetType());
        }

        [Test]
        public void MediatorThrows_onUnregisteredHandler()
        {
            var mediator = new Mediator(context, mapper);
            
            var query = new GetProductPageQuery(1, 4);            

            Assert.ThrowsAsync<HandlerNotFoundException>(async() => await mediator.Handle(query));     
        }

        [Test]
        public void RegisterFromAssembly_Registers()
        {
            //bad test. create test data in test assembly
            Mediator.RegisterFromAssembly(typeof(Mediator).Assembly);
            var query = new GetProductPageQuery(1, 4);

            Assert.DoesNotThrowAsync(async () => await mediator.Handle(query));
        }

        
    }
}
