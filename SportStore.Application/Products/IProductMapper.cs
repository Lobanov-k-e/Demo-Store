using SportStore.Application.Products.Queries;
using SportStore.Domain;
using System.Collections.Generic;

namespace SportStore.Application.Products
{
    public interface IMapper
    {
        IEnumerable<ProductDTO> MapProductsToDTO(List<Product> products);
        ProductDTO MapProductToDTO(Product product);
    }
}