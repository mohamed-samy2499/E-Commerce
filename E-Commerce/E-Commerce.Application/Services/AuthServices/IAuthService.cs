using E_Commerce.Application.DTOs.AuthDTOs;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthenticationResult> SeedRolesAsync();
        Task<AuthenticationResult> Register(RegisterDTO registerDTO);
        Task<AuthenticationResult> Login(LoginDTO loginDTO);
        Task<AuthenticationResult> Logout();
    }
}
