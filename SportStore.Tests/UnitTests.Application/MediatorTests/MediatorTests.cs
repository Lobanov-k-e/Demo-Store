using Moq;
using NUnit.Framework;
using SportStore.Application;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using System;
using System.Threading.Tasks;

namespace SportStore.Tests.UnitTests.Application.MediatorTests
{
    [TestFixture]
    class MediatorTests : TestBase
    {
        
        [Test]
        public async Task MediatorWorks()
        {
            Mediator mediator = GetMediator();
            Mediator.Register<GetProductPageQuery, GetProductPageQueryHandler>();

            var query = queryFactory.GetProductPageQuery(1, 4);

            var result = await mediator.Handle(query);
            var expected = await new GetProductPageQueryHandler(context, mapper).Handle(query);

            Assert.AreEqual(expected.GetType(), result.GetType());  
        }

        [Test]
        public void MediatorThrows_onUnregisteredHandler()
        {
            var mediator = GetMediator();

            var query = queryFactory.GetProductPageQuery(1, 4);

            Assert.ThrowsAsync<HandlerNotFoundException>(async () => await mediator.Handle(query));
        }

      

        private Mediator GetMediator()
        {
            var providerMock = new Mock<IServiceProvider>();
            providerMock.Setup(m => m.GetService(typeof(IApplicationContext))).Returns(context);
            providerMock.Setup(m => m.GetService(typeof(IMapper))).Returns(mapper);

            var mediator = new Mediator(providerMock.Object);
            return mediator;
        }

        
    }
}
