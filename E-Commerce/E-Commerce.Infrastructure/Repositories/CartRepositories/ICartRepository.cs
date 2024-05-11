using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.GenericRepositories;
using E_Commerce.Infrastructure.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories.CartRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<bool> IncreaseItem(int cartId, int itemId);
        Task<bool> RemoveItem(int cartId, int itemId);
        Task<CartResponseDTO> CartExist(string userId);
        Task<bool> AddCartItem(CartItem cartItem, int cartId);
        Task<CurrentCartInfo> GetCurrentCartInfo(string userName);
    }

}
