
using BusTracking.ViewModels.Common;
using BusTracking.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTracking.Application.System.Users
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllMonitorUnAssignAsync();
        Task<List<UserDto>> GetAllByType(int type);
        Task<PageResultDto<UserDto>> GetAllPaging(GetUserPagingRequestDto request);
        Task<ResultDto<UserDto>> GetById(Guid id);
        Task<ResponseDto> Create(CreateUserRequestDto createUserRequestDto);
        Task<ResponseDto> Update(UpdateUserRequestDto request);
        Task<ResponseDto> Delete(Guid id);
        Task<ResponseDto> AssignRoles(RoleAssignRequest request);
    }
}
