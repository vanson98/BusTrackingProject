using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Auth;
using BusTracking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.System.Auths
{
    public interface IAuthService
    {
        Task<ResultDto<string>> Authencate(LoginRequestDto loginRequest);
        Task<ResponseDto> CreateRole(string roleName);

        Task<ResultDto<List<RoleDto>>> GetAllRole();
    }
}
