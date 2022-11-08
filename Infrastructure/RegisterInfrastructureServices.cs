using Infrastructure.Context;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class RegisterInfrastructureServices
    {

        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDistributorRepository, DistributorRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ITicketRepository, TicketRepository>();
            //services.AddScoped<IHistoryRepository, HistoryRepository>();
            //services.AddScoped<IMatchRepository, MatchRepository>();
            //services.AddScoped<IPurchasedRepository, PurchasedRepository>();
            return services;
        }

    }
}
