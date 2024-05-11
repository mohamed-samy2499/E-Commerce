using E_Commerce.Infrastructure.Repositories.CartItemRepositories;
using E_Commerce.Infrastructure.Repositories.CartRepositories;
using E_Commerce.Infrastructure.Repositories.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.UnitOfWorks
{
    public class UnitOfWork(
        IProductRepository productRepository,
        ICartItemRepository cartItemRepository,
        ICartRepository cartRepository
            ) : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; } = productRepository;
        public ICartItemRepository CartItemRepository { get; set; } = cartItemRepository;
        public ICartRepository CartRepository { get; set; } = cartRepository;
    }
}
