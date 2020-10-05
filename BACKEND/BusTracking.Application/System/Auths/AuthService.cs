using BusTracking.ViewModels.System.Users;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BusTracking.Data.EF;
using BusTracking.Data.Entities;
using Microsoft.Extensions.Configuration;
using BusTracking.ViewModels.Common;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.System.Auth;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BusTracking.Application.System.Auths
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthService(BusTrackingDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
        }
        public async Task<ResultDto<string>> Authencate(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) 
                return new ResultDto<string>(ResponseCode.Validate,"Người dùng không tồn tại",null);

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) 
                return new ResultDto<string>(ResponseCode.LogicError, "Đăng nhập thất bại", null); 

            var roles = await _userManager.GetRolesAsync(user);
            // Phải chỉ định claim để authorize ở api có thể detect 
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(';',roles))
            };
            // Mã hóa Claim
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new ResultDto<string>(ResponseCode.Success,"Thành công", stringToken);
        }

        public async Task<ResponseDto> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return new ResponseDto(ResponseCode.Validate,"Role name should be provided.");
            }

            var newRole = new AppRole
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return new ResponseDto(ResponseCode.Success, "Tạo mới role thành công");
            }

            return new ResponseDto(ResponseCode.LogicError, "Đã có lỗi xảy ra");
        }

        public async Task<ResultDto<List<RoleDto>>> GetAllRole()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    NormalizedName = x.NormalizedName,
                    Description = x.Description
                }).ToListAsync();

            return new ResultDto<List<RoleDto>>(ResponseCode.Success,"Thành công",roles);
        }
    }
}
