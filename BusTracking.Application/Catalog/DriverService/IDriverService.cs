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
        Task<PageResultDto<DriverDto>> GetAllPaging(GetDriverPagingRequestDto request);
        Task<DriverDto> GetById(int busId);
        Task<int> Create(CreateDriverRequestDto requestDto);
        Task<int> Update(UpdateDriverRequestDto requestDto);
        Task<int> Delete(int driverId);
    }
}
