﻿using SportStore.Application.Categories.Commands;
using SportStore.Application.Categories.Queries;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.UnitTests.UnitTests.Application
{
    class QueryFactory
    {
        public GetProductPageQuery GetProductPageQuery(int pageNumber, int pageSize, string categoryName = null)
        {
            return new GetProductPageQuery(pageNumber, pageSize, categoryName);
        }
      

        public GetAllCategories GetAllCategoriesQuery()
        {
            return new GetAllCategories();
        }

        internal static GetCategoryById GetCategoryByIdQuery(int id)
        {
            return new GetCategoryById() { Id = id };
        }
    }
}
