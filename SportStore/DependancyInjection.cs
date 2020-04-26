using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportStore.WebUi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi
{
    public static class DependancyInjection
    {
        public static void AddWebUi(this IServiceCollection services)
        {
            services.AddScoped(provider => SessionCart.GetCart(provider));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
