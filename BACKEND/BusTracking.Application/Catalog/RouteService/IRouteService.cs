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
        Task<List<RouteDto>> GetAllRouteUnAssignAsync();
        Task<PageResultDto<RouteDto>> GetAllPagingAsync(GetRoutePagingRequestDto request);
        Task<RouteDto> GetByIdAsync(int busId);
        Task<int> CreateAsync(CreateRouteRequestDto request);
        Task<int> UpdateAsync(UpdateRouteRequestDto request);
        Task<int> DeleteAsync(int busId);
    }
}
