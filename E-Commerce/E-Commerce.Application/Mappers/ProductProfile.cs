using AutoMapper;
using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDTO>().ReverseMap();
            CreateMap<Product, ProductAddDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

        }
    }
}
