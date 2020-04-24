﻿using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using SportStore.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public class GetProductPageQuery : IRequest<ProductPageVM>
    {   //validation needed
        public GetProductPageQuery(int pageNumber, int pageSize, string categoryName)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            CurrentCategory = categoryName;
        }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string CurrentCategory { get;}
    }

    public class GetProductPageQueryHandler : RequestHandlerBase, IRequestHandler<GetProductPageQuery, ProductPageVM>
    {
      

        public GetProductPageQueryHandler(IApplicationContext context, IMapper mapper) 
            : base(context, mapper)                 
        {
        }

        public async Task<ProductPageVM> Handle(GetProductPageQuery request)
        {

            int offset = GetOffset(request);

            var products = await Context.Products
                .Include(p => p.Category)
                .Where(ByCategoryNameFilter(request))
                .OrderBy(p => p.Id)
                .Skip(offset)
                .Take(request.PageSize)
                .ToListAsync();

            int productsCount = await Context.Products
                .CountAsync(ByCategoryNameFilter(request));

            var Vm = new ProductPageVM()
            {
                Products = Mapper.MapProductsToDTO(products),
                CurrentCategory = request.CurrentCategory,
                PageInfo = new PageInfo()
                {
                    ItemsCount = productsCount,
                    CurrentPage = request.PageNumber, //проблема - отображаемая страница при < 0
                    ItemsPerPage = request.PageSize
                }
            };
            return Vm;
        }

        private static Expression<Func<Product, bool>> ByCategoryNameFilter(GetProductPageQuery request)
        {
            return p => request.CurrentCategory == null || p.Category.Name == request.CurrentCategory;
        }

        private static int GetOffset(GetProductPageQuery request)
        {
            return (request.PageNumber > 0 ? request.PageNumber - 1 : 0) * request.PageSize;
        }
       
    }
}
