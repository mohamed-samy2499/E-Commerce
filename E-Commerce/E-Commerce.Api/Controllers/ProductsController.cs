using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Application.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController:ControllerBase
    {
        #region private properties
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Endpoints
        #region Commands Endpoints
        // /Products/GetAll?filterExpressions=""
        [HttpGet]
        public async Task<IActionResult> GetAll(string? filterExpressions)
        {
            try
            {

                var mappedProducts = await _productService.GetAllProductsAsync(filterExpressions);
                return Ok(mappedProducts);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // /Products/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { ErrorMessage = "Invalid id. Please provide a valid positive integer." });
                var mappedProduct = await _productService.GetProductByIdAsync(id);
                return Ok(mappedProduct);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion
        #region Queries Endpoints
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductAddDTO model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.AddProductAsync(model, userId);
            if (result != null)
                return Ok(result);
            return BadRequest(ModelState);
        }
        #endregion
        #endregion
    }
}
