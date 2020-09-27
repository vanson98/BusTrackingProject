using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusTracking.ViewModels.Catalog.Buses;
using BusTracking.ViewModels.Common;

namespace BusTracking.Application.Catalog.BusService
{
    public interface IBusService
    {
        Task<PageResultDto<BusDto>> GetAllPagingAsync(GetBusPagingRequestDto request);
        Task<BusDto> GetByIdAsync(int busId);
        Task<int> CreateAsync(CreateBusRequestDto createBusRequestDto);
        Task<int> UpdateAsync(UpdateBusRequestDto updateBusRequestDto);
        Task<int> DeleteAsync(int busId);
    }
}
