using BusTracking.Data.Entities;
using BusTracking.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BusTracking.Data.Enum;
using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Auth;

namespace BusTracking.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
        }
        public async Task<string> Authencate(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) return null;
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(';',roles))
            };
            // Mã hóa Claim
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> CreateUser(CreateUserRequestDto request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                TypeAccount = (TypeAccount)request.TypeAccount,
                Status = (Status)request.Status,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return true;
            return false;
        }

        
    }
}
