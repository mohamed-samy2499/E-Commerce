using AutoMapper;
using E_Commerce.Application.DTOs.CartItemDTOs;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mappers
{
    public class CartItemProfile:Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemGetDTO>().ReverseMap();
            CreateMap<CartItem, CartItemAddDTO>().ReverseMap();
            CreateMap<CartItem, CartItemUpdateDTO>().ReverseMap();
        }
    }
}
