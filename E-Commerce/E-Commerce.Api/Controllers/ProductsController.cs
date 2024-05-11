using E_Commerce.Application.DTOs.ProductDTOs;
using E_Commerce.Application.Services.ProductServices;
using E_Commerce.Application.Validators.ProductValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        // /Products/Create
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductAddDTO model)
        {
            try
            {
                // Validate DTO using FluentValidation
                new ProductAddDTOValidator().ValidateAndThrow(model);
                //Getting the current logged in user Id 
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

                var result = await _productService.AddProductAsync(model, userId);
                if (result != null)
                    return Ok(result);
                return BadRequest(ModelState);
            }
            catch(FluentValidation.ValidationException ex)
            {
                //// Handle validation errors
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                //Handle other exceptions
                return BadRequest(ex.Message);
            }
        }

        // /Products/Update/5
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO model)
        {
            try
            {
                // Check if the entity with the given id exists
                var existingEntity = await _productService.GetProductByIdAsync(id);
                if (existingEntity == null)
                    return NotFound();
                // Validate DTO using FluentValidation
                new ProductUpdateDTOValidator().ValidateAndThrow(model);
                //Getting the current logged in user Id 
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

                var result = await _productService.UpdateProductAsync(model, userId);
                if (result != null)
                    return Ok(result);
                return BadRequest(ModelState);
            }
            catch (FluentValidation.ValidationException ex)
            {
                //// Handle validation errors
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                //Handle other exceptions
                return BadRequest(ex.Message);
            }
        }

        // /Products/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (id <= 0)
                return BadRequest(new { ErrorMessage = "Invalid id. Please provide a valid positive integer." });
            // Delete the entity from the repository
            var result = await _productService.DeleteProductAsync(id, userId);
            if (result == "success")
                return NoContent();

            return BadRequest(ModelState);
        }
        #endregion
        #endregion
    }
}
