using E_Commerce.Application.DTOs.CartDTOs;
using E_Commerce.Application.DTOs.CartItemDTOs;
using E_Commerce.Application.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartsController(ICartService cartService):ControllerBase
    {
        private readonly ICartService _cartService = cartService;

        #region Public Methods
        #region CommandMethods
        [HttpGet("CurrentCartInfo")]
        public async Task<IActionResult> CurrentCartInfo()
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var currentCartInfo = await _cartService.GetCurrentCartInfo(userName);
            return Ok(currentCartInfo);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { ErrorMessage = "Invalid id. Please provide a valid positive integer." });
            var cart = await _cartService.GetCartByIdAsync(id);
            return Ok(cart);
        }
        #endregion
        #region Queries Methods
        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromBody] CartItemAddDTO model)
        {
            if (model.CartId <= 0 || model.ProductId <= 0)
                return BadRequest("Invalid id. Please provide a valid positive integer.");
            var result = await _cartService.RemoveItem(model);
            if (result)
                return Ok();
            return BadRequest("Item was not removed");

        }
        [HttpPost("IncreaseItem")]
        public async Task<IActionResult> IncreaseItem([FromBody] CartItemAddDTO model)
        {
            if (model.CartId <= 0 || model.ProductId <= 0)
                return BadRequest("Invalid id. Please provide a valid positive integer.");
            var result = await _cartService.IncreaseItem(model);
            if (result)
                return Ok();
            return BadRequest("Item was not Increased");

        }
     
        [Authorize(Roles = "ADMIN")]

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartAddDTO model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.AddCartAsync(model, userId);
            if (result != null)
                return Ok(model);
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "ADMIN")]

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CartUpdateDTO model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the entity with the given id exists
            var existingEntity = await _cartService.GetCartByIdAsync(id);
            if (existingEntity == null)
                return NotFound();

            // Update the entity in the repository
            var result = await _cartService.UpdateCartAsync(model, userId);

            return Ok(result);
        }
        [Authorize(Roles = "ADMIN")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (id <= 0)
                return BadRequest(new { ErrorMessage = "Invalid id. Please provide a valid positive integer." });
            // Delete the entity from the repository
            var result = await _cartService.DeleteCartAsync(id, userId);
            if (result == "success")
                return NoContent();

            return BadRequest(ModelState);
        }

        #endregion
        #endregion
    }
}
