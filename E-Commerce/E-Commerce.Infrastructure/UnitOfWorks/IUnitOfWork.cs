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
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public ICartItemRepository CartItemRepository { get; set; }
        public ICartRepository CartRepository { get; set; }


    }
}
