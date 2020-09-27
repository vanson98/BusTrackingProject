using BusTracking.ViewModels.Catalog.Drivers;
using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.DriverService
{
    public interface IDriverService
    {
        Task<List<DriverDto>> GetAllDriverUnAssign();
        Task<PageResultDto<DriverDto>> GetAllPagingAsync(GetDriverPagingRequestDto request);
        Task<DriverDto> GetByIdAsync(int busId);
        Task<int> CreateAsync(CreateDriverRequestDto requestDto);
        Task<int> UpdateAsync(UpdateDriverRequestDto requestDto);
        Task<int> DeleteAsync(int driverId);
    }
}
