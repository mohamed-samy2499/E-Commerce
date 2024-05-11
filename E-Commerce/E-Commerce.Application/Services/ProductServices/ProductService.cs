using AutoMapper;
using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Application.Helpers;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.ApplicationDbContexts;
using E_Commerce.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.ProductServices
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region CRUD


        //Products in Database CRUD
        public async Task<ProductGetDTO> AddProductAsync(ProductAddDTO Dto, string loggedInUserId)
        {
            var entity = _mapper.Map<Product>(Dto);
            var resultEntity = await _unitOfWork.ProductRepository.CreateAsync(entity, loggedInUserId);
            var mappedEntity = _mapper.Map<ProductGetDTO>(resultEntity);
            return mappedEntity;
        }

        public async Task<string> DeleteProductAsync(int id, string loggedInUserId)
        {
            return await _unitOfWork.ProductRepository.DeleteAsync(id, loggedInUserId);
        }

        public async Task<IEnumerable<ProductGetDTO>> GetAllProductsAsync(string? filterExpressions)
        {
            //Parsing the filter string to extract the where expressions and pass them to GetAll Method
            Expression<Func<Product, bool>>[] filters = null;

            if (!string.IsNullOrEmpty(filterExpressions))
            {
                filters = filterExpressions.Split(',')
                    .Select(expression => FilterExpression.BuildFilterExpression<Product>(expression))
                    .ToArray();
            }
            var entities = await _unitOfWork.ProductRepository.GetAllAsync(filters: filters ?? []);
            var mappedEntities = _mapper.Map<IEnumerable<ProductGetDTO>>(entities);
            return mappedEntities;
        }

        public async Task<ProductGetDTO> GetProductByIdAsync(int id)
        {
            //Expression<Func<Product, object>>[] includes =
            //[
            //    
            //];
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            var mappedEntity = _mapper.Map<ProductGetDTO>(entity);
            return mappedEntity;
        }

        public async Task<ProductGetDTO> UpdateProductAsync(ProductUpdateDTO Dto, string loggedInUserId)
        {
            var entity = _mapper.Map<Product>(Dto);
            var result = await _unitOfWork.ProductRepository.UpdateAsync(entity, loggedInUserId);
            var mappedEntity = _mapper.Map<ProductGetDTO>(entity);
            return mappedEntity;
        }

        #endregion
    }
}
