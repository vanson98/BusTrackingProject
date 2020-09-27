
using BusTracking.ViewModels.System.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Application.System.Users
{
    public interface IUserService
    {
        //Task<List<UserDto>> 
        Task<bool> CreateUser(CreateUserRequestDto createUserRequestDto);
        Task<List<UserDto>> GetAllMonitorUnAssignAsync();
        Task<List<UserDto>> GetAllByType(int type);
    }
}
