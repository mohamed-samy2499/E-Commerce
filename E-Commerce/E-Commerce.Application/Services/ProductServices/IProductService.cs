using E_Commerce.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.ProductServices
{
    public interface IProductService
    {
        #region Database CRUD Signatures
        Task<IEnumerable<ProductGetDTO>> GetAllProductsAsync(string? filterExpressions);
        Task<ProductGetDTO> GetProductByIdAsync(int id);
        Task<ProductGetDTO> AddProductAsync(ProductAddDTO Dto, string loggedInUserId);
        Task<ProductGetDTO> UpdateProductAsync(ProductUpdateDTO Dto, string loggedInUserId);
        Task<string> DeleteProductAsync(int id, string loggedInUserId);
        #endregion
    }
}
