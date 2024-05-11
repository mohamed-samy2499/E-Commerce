using E_Commerce.Infrastructure.GenericRepositories;
using E_Commerce.Infrastructure.Repositories.CartItemRepositories;
using E_Commerce.Infrastructure.Repositories.CartRepositories;
using E_Commerce.Infrastructure.Repositories.ProductRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient(typeof(IProductRepository),typeof(ProductRepository));
            services.AddTransient(typeof(ICartRepository), typeof(CartRepository));
            services.AddTransient(typeof(ICartItemRepository), typeof(CartItemRepository));

            return services;
        }
    }
}
