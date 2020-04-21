﻿using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Products;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddApplication(this IServiceCollection services)       
        {            
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMediator, Mediator>();
            RegisterHandlers();
            return services;
        }

        private static void RegisterHandlers()
        {
            Mediator.Register<GetProductPageQuery, GetProductPageQueryHandler>();
        }

    }
}
