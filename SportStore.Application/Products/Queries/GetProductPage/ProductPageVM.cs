using System;
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
        public int CurrentPage { get; set; }
        public int ItemsCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)ItemsCount / ItemsPerPage);
    }
}