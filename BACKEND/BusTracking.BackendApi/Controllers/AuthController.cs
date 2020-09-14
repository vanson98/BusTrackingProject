using BusTracking.Application.System.Users;
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
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("authenticate")]
        public async Task<ResultDto<string>> Authenticate([FromBody]LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return new ResultDto<string>(400, "Dữ liệu truyền vào không đúng", null);
            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
                return new ResultDto<string>(400, "Đăng nhập không thành công", null);
            return new ResultDto<string>(200, "Đăng nhập thành công", resultToken); ;
        }
    }
}
