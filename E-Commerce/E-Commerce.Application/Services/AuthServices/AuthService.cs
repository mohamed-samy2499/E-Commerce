using E_Commerce.Application.DTOs.AuthDTOs;
using E_Commerce.Application.Settings;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.AuthServices
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager
            ) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<AuthenticationResult> SeedRolesAsync()
        {
            var returnResult = new AuthenticationResult();
            bool isUserRoleExists = await _roleManager.RoleExistsAsync("USER");
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync("ADMIN");


            if (isUserRoleExists && isAdminRoleExists)
            {
                returnResult.Succeeded = false;
                returnResult.Errors.Add("Roles Seeding is Already Done");
            }
            else
            {

                await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
                await _roleManager.CreateAsync(new IdentityRole("USER"));

                returnResult.Succeeded = true;
            }
            return returnResult;
        }
        public async Task<AuthenticationResult> Register(RegisterDTO registerDTO)
        {
            var returnResult = new AuthenticationResult();
            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                returnResult.Succeeded = false;
                returnResult.Errors.Add("Email is already registered!");
                return returnResult;

            }

            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                returnResult.Succeeded = false;
                returnResult.Errors.Add("Username is already registered!");
                return returnResult;

            }
            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {

                returnResult.Succeeded = false;
                foreach (var error in result.Errors)
                    returnResult.Errors.Add($"{error.Description},");
                return returnResult;

            }
            // Assign the role 'USER' to the user
            await _userManager.AddToRoleAsync(user, "USER");
            returnResult.Succeeded = true;
            var jwtSecurityToken = await CreateJwtToken(user);
            returnResult.Email = user.Email;
            returnResult.ExpiresOn = jwtSecurityToken.ValidTo;
            returnResult.IsAuthenticated = true;
            returnResult.Roles = new List<string> { "USER" };
            returnResult.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            returnResult.Username = user.UserName;
            return returnResult;
        }
        public async Task<AuthenticationResult> Login(LoginDTO loginDTO)
        {
            var returnResult = new AuthenticationResult();

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                returnResult.Errors.Add("Email or Password is incorrect!");
                return returnResult;
            }

            //var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);
            returnResult.Succeeded = true;
            returnResult.IsAuthenticated = true;
            returnResult.Token = await GenerateJwtToken(user);
            returnResult.Email = user.Email;
            returnResult.Username = user.UserName;
            returnResult.ExpiresOn = DateTime.Now.AddDays(JwtSettings.DurationInDays);
            returnResult.Roles = rolesList.ToList();

            return returnResult;
        }
        public async Task<AuthenticationResult> Logout()
        {
            var returnResult = new AuthenticationResult();

            try
            {
                // Use SignInManager to sign out the user
                await _signInManager.SignOutAsync();

                returnResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                returnResult.Succeeded = false;
                returnResult.Errors.Add($"Logout failed: {ex.Message}");
            }

            return returnResult;
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSecurityToken = await CreateJwtToken(user);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = userRoles.Select(r => new Claim("roles", r)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(JwtSettings.DurationInDays!),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
