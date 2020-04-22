using SportStore.Application.Products.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SportStore.UnitTests.UnitTests.Application.CategoriesTests
{
    class CategoryDTOByIdComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return Compare(x as CategoryDTO, y as CategoryDTO);
        }
        public int Compare([AllowNull] CategoryDTO x, [AllowNull] CategoryDTO y)
        {
            if (x is null || y is null)
                return -1; // throw?
            if (x.Id > y.Id)
                return 1;
            else if (x.Id < y.Id)
                return -1;
            return 0;
        }


    }   
}
