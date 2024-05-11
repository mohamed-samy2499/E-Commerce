using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.ApplicationDbContexts;
using E_Commerce.Infrastructure.GenericRepositories;
using E_Commerce.Infrastructure.Helpers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories.CartRepositories
{
    public class CartRepository(ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager):
        GenericRepository<Cart>(dbContext),
        ICartRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<bool> RemoveItem(int cartId, int itemId)
        {
            var cart = await _dbContext.Carts.FindAsync(cartId);
            var relation = await _dbContext.CartItems.FindAsync(itemId);
            if (relation != null)
            {
                if (cart != null)
                {
                    cart.TotalPrice -= relation.Price;
                    _dbContext.Entry(cart).State = EntityState.Modified;
                }
                if (relation.Quantity > 1)
                {

                    relation.Quantity--;
                }
                else
                {

                    _dbContext.CartItems.Remove(relation);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> IncreaseItem(int cartId, int itemId)
        {
            var cart = await _dbContext.Carts.FindAsync(cartId);
            var relation = await _dbContext.CartItems.FindAsync(itemId);
            if (relation != null)
            {
                if (relation.Quantity >= 1)
                {
                    relation.Quantity++;
                    if (cart != null)
                    {

                        cart.TotalPrice += relation.Price;
                        _dbContext.Entry(cart).State = EntityState.Modified;
                    }
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

            }
            return false;
        }
        public override async Task<Cart> GetByIdAsync(int id, Expression<Func<Cart, object>>[] includes = null)
        {
            var query = _dbContext.Set<Cart>().AsQueryable();

            // Apply includes if provided
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var entity = await query
                .Include(sc => sc.CartItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            return entity;
        }
        public override async Task<Cart> CreateAsync(Cart entity, string? loggedInUserId = null)
        {
            if (!string.IsNullOrEmpty(loggedInUserId))
            {
                var user = await _userManager.FindByNameAsync(loggedInUserId);
                entity.CreatedBy = user?.Id ?? "";
            }
            await _dbContext.Carts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> AddCartItem(CartItem cartItem, int cartId)
        {
            var cart = await _dbContext.Carts.Include(sc => sc.CartItems).FirstOrDefaultAsync(sc => sc.Id == cartId);
            if (cart == null)
                return false;
            if (cart.TotalPrice == null)
                cart.TotalPrice = 0;
            //check if the cartItem already exists 
            var existingCartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            //increase the quantity if the cartItem already exists

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
                cart.TotalPrice += existingCartItem.Price;
                _dbContext.Entry(cart).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return true;
            }
            //else add the cartItem
            cart.TotalPrice += cartItem.Price;
            cart.CartItems.Add(cartItem);
            _dbContext.Entry(cart).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CartResponseDTO> CartExist(string userId)
        {
            var result = await _dbContext.Carts.Include(s => s.User).Where(s => s.UserId == userId).FirstOrDefaultAsync();
            var response = new CartResponseDTO
            {
                Exist = false
            };
            if (result == null)
                return response;
            response.CartId = result.Id;
            response.Exist = true;
            return response;
        }

        public async Task<CurrentCartInfo> GetCurrentCartInfo(string userName)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);
            var userId = currentUser?.Id;
            //find the cart of the currentuser and create one if it doesnt exist
            var cartExist = await CartExist(userId);
            if (!cartExist.Exist)
            {
                var entity = await CreateAsync(new Cart { UserId = userId }, userName);
                await _dbContext.SaveChangesAsync();
                var cartId = entity.Id;
                var ret = new CurrentCartInfo
                {
                    Id = cartId,
                    ItemsCount = 0
                };
                return ret;
            }
            var currentCart = await GetByIdAsync(cartExist.CartId);
            var ret1 = new CurrentCartInfo
            {
                Id = cartExist.CartId,
                ItemsCount = currentCart.CartItems.Sum(item => item.Quantity)
            };
            return ret1;

        }
    }
}
