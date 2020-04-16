using SportStore.Application.Interfaces;
using System.Collections.Generic;

namespace SportStore.Application.Products.Queries
{
    public interface IProductQueryFactory
    {
        IQuery<IEnumerable<ProductDTO>> GetAllQuery();
        IQuery<IEnumerable<ProductDTO>> GetProductPageQuery(int pageNumber, int pageSize);
    }
}