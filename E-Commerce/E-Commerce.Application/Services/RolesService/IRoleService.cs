using E_Commerce.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.RolesService
{
    public interface IRoleService
    {
        List<IdentityRole> GetAllRoles();
        Task<RoleDetails> GetAllUsersInRole(string roleId);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(string roleId, IdentityRole roleModel);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<IdentityRole> GetRoleAsync(string roleId);
        Task<bool> AssignRoleToUsersAsync(List<UserInRoleDTO> userInRoleDTOs, string roleId);
        Task<List<UserInRoleDTO>> GetAllUsers(string roleId);
    }
}
