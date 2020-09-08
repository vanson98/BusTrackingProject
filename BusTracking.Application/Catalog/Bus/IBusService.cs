using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusTracking.ViewModels.Catalog.Buses;
using BusTracking.ViewModels.Common;

namespace BusTracking.Application.Catalog.Bus
{
    public interface IBusService
    {
        Task<PageResultDto<BusDto>> GetAllPaging(GetBusPagingRequestDto request);
        Task<BusDto> GetById(int busId); 
        Task<int> Create(CreateBusRequestDto createBusRequestDto);
        Task<int> Update(UpdateBusRequestDto updateBusRequestDto);
        Task<int> Delete(int busId);
    }
}
