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

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<UserDto>> GetAllPaging([FromQuery] GetUserPagingRequestDto request)
        {
            var result = await _userService.GetAllPaging(request);
            return result;
        }
        
        [HttpGet("GetById")]
        public async Task<ResultDto<UserDto>> GetById([FromQuery] Guid id)
        {
            var result = await this._userService.GetById(id);
            return result;
        }

        [HttpPost("CreateUser")]
        public async Task<ResponseDto> CreateUser([FromBody]CreateUserRequestDto request)
        {
            if (!ModelState.IsValid)
                return new ResponseDto(ResponseCode.Validate,"Đầu vào không hợp lệ");
            var result = await _userService.Create(request);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody]UpdateUserRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _userService.Update(request);
            return result;
        }

        [HttpDelete("Delete")]
        public async Task<ResponseDto> Delete([FromQuery]Guid id)
        {
            var result = await _userService.Delete(id);
            return result;
        }

        [HttpPut("AssignRole")]
        public async Task<ResponseDto> AssignRole([FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _userService.AssignRoles(request);
            return result;
        }
    }
}
