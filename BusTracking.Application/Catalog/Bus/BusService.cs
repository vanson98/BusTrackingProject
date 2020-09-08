using BusTracking.ViewModels.Catalog.Buses;
using BusTracking.ViewModels.Common;
using System;
using System.Threading.Tasks;
using BusTracking.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BusTracking.Application.Catalog.Bus
{
    public class BusService : IBusService
    {
        private readonly BusTrackingDbContext _context;

        public BusService(BusTrackingDbContext context)
        {
            this._context = context;
        }

        public async Task<PageResultDto<BusDto>> GetAllPaging(GetBusPagingRequestDto request)
        {
            // Select and Join 
            var query = from b in _context.Buses
                        join dr in _context.Drivers on b.DriverId equals dr.Id
                        join m in _context.AppUsers on b.MonitorId equals m.Id
                        join r in _context.Routes on b.RouteId equals r.Id
                        select new { b, dr, m, r };
            // Filter
            if (request.LicenseCode != null)
            {
                query = query.Where(x => x.b.LicenseCode == request.LicenseCode);
            }
            if(request.Status != null)
            {
                query = query.Where(x => (int)x.b.Status == request.Status);
            }
            if (request.DriverId != null)
            {
                query = query.Where(x => x.b.DriverId == request.DriverId);
            }
            if (request.RouteId != null)
            {
                query = query.Where(x => x.b.RouteId == request.RouteId);
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x=> new BusDto() { 
                                    Id = x.b.Id,
                                    LicenseCode = x.b.LicenseCode,
                                    MaxSize = x.b.MaxSize,
                                    MaxSpeed = x.b.MaxSpeed,
                                    Description = x.b.Description,
                                    Status = (int)x.b.Status,
                                    DriverId = x.dr.Id,
                                    DriverName = x.dr.Name,
                                    MonitorId = x.m.Id,
                                    MonitorName = x.m.FullName,
                                    RouteId = x.r.Id,
                                    RouteName = x.r.Name
                                  })
                                  .ToListAsync();
            // Return 
            var pageResutlDto = new PageResultDto<BusDto>()
            {
                Items = data,
                Message="Success",
                TotalRecord = totalRow
            };
            return pageResutlDto;
        }

        public Task<BusDto> GetById(int busId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(CreateBusRequestDto createBusRequestDto)
        {
            throw new NotImplementedException();
        }
        public Task<int> Update(UpdateBusRequestDto updateBusRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int busId)
        {
            throw new NotImplementedException();
        }

        
    }
}
