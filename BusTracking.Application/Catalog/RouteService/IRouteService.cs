using BusTracking.ViewModels.Catalog.Routes;
using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.RouteService
{
    public interface IRouteService
    {
        Task<PageResultDto<RouteDto>> GetAllPaging(GetRoutePagingRequestDto request);
        Task<RouteDto> GetById(int busId);
        Task<int> Create(CreateRouteRequestDto request);
        Task<int> Update(UpdateRouteRequestDto request);
        Task<int> Delete(int busId);
    }
}
