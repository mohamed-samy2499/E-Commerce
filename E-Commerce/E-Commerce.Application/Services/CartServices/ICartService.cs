using E_Commerce.Application.DTOs.CartDTOs;
using E_Commerce.Application.DTOs.CartItemDTOs;
using E_Commerce.Application.DTOs.CartDTOs;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.CartServices
{
    public interface ICartService
    {
        Task<bool> IncreaseItem(CartItemAddDTO model);
        Task<bool> RemoveItem(CartItemAddDTO model);
        Task<CartResponseDTO> CartExist(string userId);
        Task<bool> AddCartItem(CartItemAddDTO model, int cartId);
        Task<CurrentCartInfo> GetCurrentCartInfo(string userName);
        Task<CartGetDTO> GetCartByIdAsync(int id);
        Task<CartGetDTO> AddCartAsync(CartAddDTO Dto, string loggedInUserId);
        Task<CartGetDTO> UpdateCartAsync(CartUpdateDTO Dto, string loggedInUserId);
        Task<string> DeleteCartAsync(int id, string loggedInUserId);
    }
}
