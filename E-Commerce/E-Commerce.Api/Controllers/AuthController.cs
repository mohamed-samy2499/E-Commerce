using E_Commerce.Application.DTOs.AuthDTOs;
using E_Commerce.Application.DTOs.AuthDTOs.AuthDTOsValidators;
using E_Commerce.Application.Services.AuthServices;
using E_Commerce.Application.Validators.ProductValidators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService):ControllerBase
    {
        private readonly IAuthService _authService = authService;

        //Seed The Roles "ADMIN" "USER" API
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                var result = await _authService.SeedRolesAsync();
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Register API
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                // Validate DTO using FluentValidation
                new RegisterDTOValidator().ValidateAndThrow(registerDTO);

                // Call the register service
                var result = await _authService.Register(registerDTO);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(new { result.Errors });
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
                return BadRequest(ex.Message);

            }
            // Use FluentValidation to validate the DTO

        }

        //Login API
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Validate DTO using FluentValidation
                new LoginDTOValidator().ValidateAndThrow(loginDTO);

                // Call the login service
                var result = await _authService.Login(loginDTO);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return Unauthorized(new { result.Errors });
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
                return BadRequest(ex.Message);
            }
           
        }

        //Logout API
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout(); 

                return Ok(new { Message = "Logout successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
