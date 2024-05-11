using E_Commerce.Application.DTOs.AuthDTOs;
using E_Commerce.Application.DTOs.AuthDTOs.AuthDTOsValidators;
using E_Commerce.Application.Services.RolesService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class RolesController(IRoleService roleService)
        :ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        //Get All Roles API
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);
        }

        //Get A Role By ID API
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var result = await _roleService.GetAllUsersInRole(id);
            return Ok(result);
        }

        //Delete a role API
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _roleService.DeleteRoleAsync(id);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            if (model.Id != id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleService.UpdateRoleAsync(id, model);
                    if (result)
                        return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _roleService.CreateRoleAsync(role.Name);
                    if (result)
                        return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        //get a list of all users and showing wether each user is selected within this role or  not 
        [HttpGet("AddOrRemoveUsers/{roleId}")]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            if (roleId == null)
                return NotFound();

            var role = await _roleService.GetRoleAsync(roleId);

            if (role == null)
                return NotFound();

            var allUsers = await _roleService.GetAllUsers(roleId);

            return Ok(allUsers);
        }
        //Assigning or removing users from a role

        [HttpPut("AddOrRemoveUsers/{roleId}")]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleDTO> model, string roleId)
        {
            try
            {
                foreach(var item in model)
                    // Validate DTO using FluentValidation
                    new UserInRoleDTOValidator().ValidateAndThrow(item);
                var result = await _roleService.AssignRoleToUsersAsync(model, roleId);
                if (result)
                    return Ok();
                return BadRequest();
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
    }
}
