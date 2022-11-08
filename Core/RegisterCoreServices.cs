using Core.Interfaces.Services;
using Core.Services.Distributor;
using Core.Services.Product;
using Core.Services.Sales;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class RegisterCoreServices
    {

        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddScoped<IDistributorService, DistributorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISalesService, SalesService>();

            return services;
        }

    }
}
