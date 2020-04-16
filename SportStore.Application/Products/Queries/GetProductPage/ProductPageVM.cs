using System.Collections.Generic;

namespace SportStore.Application.Products.Queries
{
    public class ProductPageVM
    {      
        public PageInfo PageInfo { get; set; }
        public IEnumerable<ProductDTO> Products { get; internal set; }
    }

    public class PageInfo
    {
        public int CurrentPage { get; internal set; }
        public int ProductsCount { get; internal set; }
    }
}