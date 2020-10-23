using BusTracking.Application.System.Auths;
using BusTracking.Application.System.Users;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Auth;
using BusTracking.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService userService)
        {
            _authService = userService;
        }

        [HttpPost("WebAuthenticate")]
        [AllowAnonymous]
        public async Task<ResultDto<AuthenticateResultModel>> WebAuthenticate([FromBody]LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return new ResultDto<AuthenticateResultModel>(ResponseCode.Validate, "Dữ liệu truyền vào không đúng", null);
            var res = await _authService.Authencate(request);
            if (res.Result!=null && res.Result.TypeAccount != 2)
            {
                return new ResultDto<AuthenticateResultModel>(ResponseCode.Validate, "Tên đăng nhập hoặc mật khẩu không đúng", null);
            }
            return res;
        }

        [HttpPost("AppAuthenticate")]
        [AllowAnonymous]
        public async Task<ResultDto<AuthenticateResultModel>> AppAuthenticate([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return new ResultDto<AuthenticateResultModel>(ResponseCode.Validate, "Dữ liệu truyền vào không đúng", null);
            var res = await _authService.Authencate(request);
            if (res.Result != null && res.Result.TypeAccount == 2)
            {
                return new ResultDto<AuthenticateResultModel>(ResponseCode.Validate, "Tên đăng nhập hoặc mật khẩu không đúng", null);
            }
            return res;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("GetAllRole")]
        public async Task<ResultDto<List<RoleDto>>> GetAll()
        {
            var result = await _authService.GetAllRole();
            return result;
        }

        [Authorize]
        [HttpGet("GetUserSession")]
        public async Task<ResultDto<UserSessionDto>> GetUserSession()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId =  identity.FindFirst("userId").Value;
                var result = await _authService.GetUserSession(userId);
                return result;
            }
            else
            {
                return new ResultDto<UserSessionDto>(ResponseCode.LogicError, "Thất bại", null);
            }
            
        }
    }
}
