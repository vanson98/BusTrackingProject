using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTracking.Application.System.Users;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllMonitorUnAssign")]
        public async Task<ResultDto<List<UserDto>>> GetAllMonitorUnAssign()
        {
            var monitors = await _userService.GetAllMonitorUnAssignAsync();
            return new ResultDto<List<UserDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thực thi thành công",
                Result = monitors
            };
        }

        [HttpGet("GetAllByType")]
        public async Task<ResultDto<List<UserDto>>> GetAllByType([FromQuery]int type)
        {
            var user = await _userService.GetAllByType(type);
            return new ResultDto<List<UserDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thực thi thành công",
                Result = user
            };
        }

        [HttpPost("CreateUser")]

        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _userService.CreateUser(request);
            if (!result)
                return BadRequest("Create user fail!");
            return Ok();
        }

        
    }
}
