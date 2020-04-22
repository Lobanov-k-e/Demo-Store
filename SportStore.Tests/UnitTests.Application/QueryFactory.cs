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
    }
}
