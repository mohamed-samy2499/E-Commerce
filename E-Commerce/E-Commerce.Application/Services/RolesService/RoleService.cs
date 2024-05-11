using E_Commerce.Application.DTOs.AuthDTOs;
using E_Commerce.Application.Services.AuthServices;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.RolesService
{
    public class RoleService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager
            ) : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<bool> AssignRoleToUsersAsync(List<UserInRoleDTO> userInRoleDTOs, string roleId)
        {
            try
            {

                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                    return false;
                foreach (var user in userInRoleDTOs)
                {
                    var FullUser = await _userManager.FindByIdAsync(user.UserId);
                    if (FullUser == null)
                        continue;
                    if (!user.IsSelected && await _userManager.IsInRoleAsync(FullUser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(FullUser, role.Name);
                    }
                    else if (user.IsSelected && !await _userManager.IsInRoleAsync(FullUser, role.Name))
                    {
                        await _userManager.AddToRoleAsync(FullUser, role.Name);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            IdentityRole role = new IdentityRole
            {
                Name = roleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var result = await _roleManager.FindByIdAsync(roleId);
            if (result == null)
                return false;
            var deleteResult = await _roleManager.DeleteAsync(result);
            if (deleteResult.Succeeded)
                return true;
            return false;
        }

        public async Task<IdentityRole> GetRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            //
            return role;
        }
        //get All users list and select the ones within this role
        public async Task<List<UserInRoleDTO>> GetAllUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var users = await _userManager.Users.ToListAsync();
            List<UserInRoleDTO> usersInRole = new List<UserInRoleDTO>();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleDTO()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                usersInRole.Add(userInRole);
            }
            return usersInRole;
        }

        public async Task<bool> UpdateRoleAsync(string roleId, IdentityRole roleModel)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;
            role.Name = roleModel.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return true;
            return false;
        }

        public List<IdentityRole> GetAllRoles()
        {
            var result = _roleManager.Roles;
            return [.. result];
        }

        public async Task<RoleDetails> GetAllUsersInRole(string roleId)
        {
            var result = new RoleDetails();
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return result;

            result.RoleName = role.Name;
            result.RoleId = role.Id;

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var UserInRole = new UserInRoleDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    result.Users.Add(UserInRole);
            }

            return result;
        }
    }
}
