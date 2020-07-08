using SportStore.Application.Products.Queries;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SportStore.UnitTests.Application.ProductTests
{ 
    class ProductDTOByIdComparer : IComparer
    {
        public int Compare([AllowNull] ProductDTO x, [AllowNull] ProductDTO y)
        {
            if (x is null || y is null)
                return -1; // throw?
            if (x.Id > y.Id)
                return 1;
            else if (x.Id < y.Id)            
                return -1;            
            return 0;

        }

        public int Compare(object x, object y)
        {
            return Compare(x as ProductDTO, y as ProductDTO);
        }
    }
}
