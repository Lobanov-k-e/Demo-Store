using Moq;
using NUnit.Framework;
using SportStore.Application;
using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using SportStore.WebUi.Controllers;
using System.Threading.Tasks;

namespace SportStore.UnitTests.UnitTests.WebUi
{
    [TestFixture]
    class ProductControllerTests
    {
        [Test]
        public void ProductList_Send_CorrectCommand_WithDefaults()
        {
            var mediatorMock = new Mock<IMediator>();            
            GetProductPageQuery query = null;
            mediatorMock.Setup(c => c.Handle<ProductPageVM>(It.IsAny<GetProductPageQuery>()))
                    .Callback<IRequest<ProductPageVM>>((pq) => query = pq as GetProductPageQuery)
                    .Returns(Task.FromResult(new ProductPageVM()));
            var controller = new ProductController(mediatorMock.Object);

            _ = controller.ProductList();

            const int DefaultPageNumber = 1;
            Assert.That(query.PageNumber, Is.EqualTo(DefaultPageNumber));

            const int DefautPageSize = 3;
            Assert.That(query.PageSize, Is.EqualTo(DefautPageSize));
        }

        [Test]
        public void ProductList_Send_CorrectCommand()
        {
            var mediatorMock = new Mock<IMediator>();
            GetProductPageQuery query = null;
            mediatorMock.Setup(c => c.Handle<ProductPageVM>(It.IsAny<GetProductPageQuery>()))
                    .Callback<IRequest<ProductPageVM>>((pq) => query = pq as GetProductPageQuery)
                    .Returns(Task.FromResult(new ProductPageVM()));
            var controller = new ProductController(mediatorMock.Object);

            const int PageNumber = 3;
            _ = controller.ProductList(PageNumber);    
            
            Assert.That(query.PageNumber, Is.EqualTo(PageNumber));

            const int DefautPageSize = 3;
            Assert.That(query.PageSize, Is.EqualTo(DefautPageSize));
        }
    }
}
