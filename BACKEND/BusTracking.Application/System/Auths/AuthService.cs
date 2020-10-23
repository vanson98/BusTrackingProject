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
        private readonly BusTrackingDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(BusTrackingDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            this._context = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._config = config;
        }
        public async Task<ResultDto<AuthenticateResultModel>> Authencate(LoginRequestDto request)
        {
            var user = await _context.AppUsers.Where(x=>x.Status!=0 && x.IsDeleted==false && x.UserName==request.UserName).FirstOrDefaultAsync();
            if (user == null) 
                return new ResultDto<AuthenticateResultModel>(ResponseCode.Validate,"Người dùng không tồn tại hoặc đã bị khóa",null);

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded) 
                return new ResultDto<AuthenticateResultModel>(ResponseCode.LogicError, "Đăng nhập thất bại", null); 

            var roles = await _userManager.GetRolesAsync(user);
            // Phải chỉ định claim để authorize ở api có thể detect 
            var claims = new[]
            {
                new Claim("email",user.Email),
                new Claim("userId",user.Id.ToString()),
                new Claim("roles", string.Join(';',roles)),

            };
            // Mã hóa Claim
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
                );
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            var authResultModel = new AuthenticateResultModel()
            {
                UserId = user.Id.ToString(),
                FullName = user.FullName,
                TypeAccount = (int)user.TypeAccount,
                Email = user.Email,
                Roles = roles,
                AccessToken = stringToken
            };
            return new ResultDto<AuthenticateResultModel>(ResponseCode.Success,"Thành công",authResultModel);
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
                    DisplayName = x.DisplayName,
                    Description = x.Description
                }).ToListAsync();

            return new ResultDto<List<RoleDto>>(ResponseCode.Success,"Thành công",roles);
        }

        public async Task<ResultDto<UserSessionDto>> GetUserSession(string userId)
        {
            var id = Guid.Parse(userId);
            var user = await _context.AppUsers.FindAsync(id);
            if (user == null)
                return new ResultDto<UserSessionDto>(ResponseCode.Validate, "Người dùng không tồn tại hoặc đã bị khóa", null);
            var roles = await _userManager.GetRolesAsync(user);
            var session = new UserSessionDto()
            {
                UserId = user.Id.ToString(),
                FullName = user.FullName,
                Roles = roles,
                Email = user.Email
            };
            return new ResultDto<UserSessionDto>(ResponseCode.Success, "Thành công", session);
        }
    }
}
