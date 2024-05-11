using E_Commerce.Application.Mappers;
using E_Commerce.Application.Services.AuthServices;
using E_Commerce.Application.Services.CartServices;
using E_Commerce.Application.Services.ProductServices;
using E_Commerce.Application.Services.RolesService;
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
            services.AddTransient(typeof(IAuthService), typeof(AuthService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(ICartService), typeof(CartService));

            //Register Mapper Profiles
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));
            services.AddAutoMapper(M => M.AddProfile(new CartItemProfile()));



            return services;
        }
    }
}
