using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Auth;
using BusTracking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequestDto loginRequest);
        Task<bool> CreateUser(CreateUserRequestDto createUserRequestDto);
        
    }
}
