using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTracking.Application.System.Users;
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken))
                return BadRequest("Login fail!");
            return Ok(resultToken);
        }

        [HttpPost("create-user")]
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
