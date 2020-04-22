using SportStore.Application.Products.Queries;
using SportStore.Domain;
using System.Collections.Generic;

namespace SportStore.Application.Interfaces
{
    public interface IMapper
    {
        IEnumerable<CategoryDTO> MapCategoriesToDTO(List<Category> categories);
        CategoryDTO MapCategoryToDTO(Category category);
        IEnumerable<ProductDTO> MapProductsToDTO(List<Product> products);
        ProductDTO MapProductToDTO(Product product);
    }
}