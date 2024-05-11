using E_Commerce.Application.Services.ProductServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            //Register Auto Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Register My Services
            services.AddTransient(typeof(IProductService),typeof(ProductService));
            return services;
        }
    }
}
