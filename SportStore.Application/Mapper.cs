using SportStore.Application.Interfaces;
using SportStore.Application.Products.Queries;
using SportStore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Application.Products
{
    public class Mapper : IMapper
    {
        public IEnumerable<ProductDTO> MapProductsToDTO(List<Product> products)
        {
            return (products.Select(product => MapProductToDTO(product))).ToList();
        }

        public ProductDTO MapProductToDTO(Product product)
        {

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = MapCategoryToDTO(product.Category)
            };
        }

        public CategoryDTO MapCategoryToDTO(Category category)
        {
            return new CategoryDTO()
            {
                Id = category?.Id ?? 0,
                Name = category?.Name,
                Description = category?.Description
            };
        }

        public IEnumerable<CategoryDTO> MapCategoriesToDTO(List<Category> categories)
        {
            return categories.Select(c => MapCategoryToDTO(c)).ToList();
        }
    }
}
