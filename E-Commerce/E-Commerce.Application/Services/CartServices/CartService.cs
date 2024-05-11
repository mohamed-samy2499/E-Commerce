using AutoMapper;
using E_Commerce.Application.DTOs.CartDTOs;
using E_Commerce.Application.DTOs.CartItemDTOs;
using E_Commerce.Application.DTOs.CartDTOs;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Helpers.Models;
using E_Commerce.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.DTOs.CartDTOs;

namespace E_Commerce.Application.Services.CartServices
{
    public class CartService(IUnitOfWork unitOfWork,IMapper mapper) : ICartService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddCartItem(CartItemAddDTO model, int cartId)
        {
            var cartItem = _mapper.Map<CartItem>(model);
            var result = await _unitOfWork.CartRepository.AddCartItem(cartItem, cartId);
            return result;
        }

        public Task<CartResponseDTO> CartExist(string userId)
        => _unitOfWork.CartRepository.CartExist(userId);

        public async Task<CartGetDTO> GetCartByIdAsync(int id)
        {
            var result = await _unitOfWork.CartRepository.GetByIdAsync(id);
            return _mapper.Map<CartGetDTO>(result);
        }

        public Task<CurrentCartInfo> GetCurrentCartInfo(string userName)
        => _unitOfWork.CartRepository.GetCurrentCartInfo(userName);


        public Task<bool> IncreaseItem(CartItemAddDTO model)
        => _unitOfWork.CartRepository.IncreaseItem(model.CartId,model.CartId);

        public Task<bool> RemoveItem(CartItemAddDTO model)
        => _unitOfWork.CartRepository.RemoveItem(model.CartId, model.CartId);


        public async Task<CartGetDTO> AddCartAsync(CartAddDTO Dto, string loggedInUserId)
        {
            var entity = _mapper.Map<Cart>(Dto);
            var resultEntity = await _unitOfWork.CartRepository.CreateAsync(entity, loggedInUserId);
            var mappedEntity = _mapper.Map<CartGetDTO>(resultEntity);
            return mappedEntity;
        }

        public async Task<string> DeleteCartAsync(int id, string loggedInUserId)
        {
            return await _unitOfWork.CartRepository.DeleteAsync(id, loggedInUserId);
        }
        public async Task<CartGetDTO> UpdateCartAsync(CartUpdateDTO Dto, string loggedInUserId)
        {
            var entity = _mapper.Map<Cart>(Dto);
            var result = await _unitOfWork.CartRepository.UpdateAsync(entity, loggedInUserId);
            var mappedEntity = _mapper.Map<CartGetDTO>(entity);
            return mappedEntity;
        }
    }
}
