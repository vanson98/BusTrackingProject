using BusTracking.Data.Enum;
using BusTracking.ViewModels.Catalog.Stops;
using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.StopService
{
    public interface IStopService
    {
        Task<PageResultDto<StopDto>> GetAllPaging(GetStopPagingReqestDto request);
        Task<ResultDto<List<StopDto>>> GetAllByBus(int busId,int typeStop);
        Task<StopDto> GetById(int busId);
        Task<int> Create(CreateStopRequestDto request);
        Task<int> Update(UpdateStopRequestDto request);
        Task<int> Delete(int id);
    }
}
