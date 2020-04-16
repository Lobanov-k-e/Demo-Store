using SportStore.Application.Interfaces;
using System.Collections.Generic;

namespace SportStore.Application.Products.Queries
{
    public interface IProductQueryFactory
    {
        IRequest<IEnumerable<ProductDTO>> GetAllQuery();
        IRequest<IEnumerable<ProductDTO>> GetProductPageQuery(int pageNumber, int pageSize);
    }
}