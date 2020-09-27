using BusTracking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.System.Auths
{
    public interface IAuthService
    {
        Task<string> Authencate(LoginRequestDto loginRequest);
    }
}
