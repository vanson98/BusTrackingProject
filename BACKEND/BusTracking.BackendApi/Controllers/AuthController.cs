using BusTracking.Application.System.Auths;
using BusTracking.Application.System.Users;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Auth;
using BusTracking.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("authenticate")]
        public async Task<ResultDto<string>> Authenticate([FromBody]LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return new ResultDto<string>(ResponseCode.Validate, "Dữ liệu truyền vào không đúng", null);
            var resultToken = await _authService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
                return new ResultDto<string>(ResponseCode.AuthFail, "Đăng nhập không thành công", null);
            return new ResultDto<string>(ResponseCode.Success, "Đăng nhập thành công", resultToken); ;
        }
    }
}
