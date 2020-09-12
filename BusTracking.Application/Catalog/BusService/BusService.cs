using BusTracking.ViewModels.Catalog.Buses;
using BusTracking.ViewModels.Common;
using System;
using System.Threading.Tasks;
using BusTracking.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusTracking.Utilities;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;

namespace BusTracking.Application.Catalog.BusService
{
    public class BusService : IBusService
    {
        private readonly BusTrackingDbContext _context;

        public BusService(BusTrackingDbContext context)
        {
            _context = context;
        }

        public async Task<PageResultDto<BusDto>> GetAllPaging(GetBusPagingRequestDto request)
        {
            // Select and Join 
            var query = from b in _context.Buses
                        where b.IsDeleted == false
                        from dr in _context.Drivers.Where(dr => dr.Id == b.DriverId).DefaultIfEmpty()
                        from m in _context.AppUsers.Where(m => b.MonitorId == m.Id).DefaultIfEmpty()
                        from r in _context.Routes.Where(r => b.RouteId == r.Id).DefaultIfEmpty()
                        select new { b, dr, m, r };
            // Filter
            if (request.LicenseCode != null)
            {
                query = query.Where(x => x.b.LicenseCode.Contains(request.LicenseCode));
            }
            if (request.Status != null)
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
            var data = await query.Skip((request.PageIndex-1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new BusDto()
                                  {
                                      Id = x.b.Id,
                                      LicenseCode = x.b.LicenseCode,
                                      MaxSize = x.b.MaxSize,
                                      MaxSpeed = x.b.MaxSpeed,
                                      Description = x.b.Description,
                                      Status = (int)x.b.Status,
                                      DriverId = x.dr == null ? -1 : x.dr.Id,
                                      DriverName = x.dr == null ? null : x.dr.Name,
                                      MonitorId = x.m == null ? Guid.Empty : x.m.Id,
                                      MonitorName = x.m == null ? null : x.m.FullName,
                                      RouteId = x.r == null ? -1 : x.r.Id,
                                      RouteName = x.r == null ? null : x.r.Name
                                  })
                                  .ToListAsync();
            // Return 
            var pageResutlDto = new PageResultDto<BusDto>()
            {
                Items = data,
                Message = "Success",
                TotalRecord = totalRow
            };
            return pageResutlDto;
        }

        public async Task<BusDto> GetById(int busId)
        {
            // Select and Join 
            var query = from b in _context.Buses where b.Id == busId && b.IsDeleted == false
                        from dr in _context.Drivers.Where(dr => dr.Id == b.DriverId).DefaultIfEmpty()
                        from m in _context.AppUsers.Where(m => b.MonitorId == m.Id).DefaultIfEmpty()
                        from r in _context.Routes.Where(r=>b.RouteId == r.Id).DefaultIfEmpty()
                        select new { b, dr, m, r };
            var x = await query.FirstOrDefaultAsync();
            if (x == null) throw new BusTrackingException($"Can't not find any object with id is {busId}");
            return new BusDto()
            {
                Id = x.b.Id,
                LicenseCode = x.b.LicenseCode,
                MaxSize = x.b.MaxSize,
                MaxSpeed = x.b.MaxSpeed,
                Description = x.b.Description,
                Status = (int)x.b.Status,
                DriverId = x.dr ==null ? -1 : x.dr.Id,
                DriverName = x.dr == null ? null : x.dr.Name,
                MonitorId = x.m == null ? Guid.Empty : x.m.Id,
                MonitorName = x.m == null ? null:  x.m.FullName,
                RouteId = x.r ==null ? -1 : x.r.Id,
                RouteName = x.r == null? null :  x.r.Name
            };
        }

        public async Task<int> Create(CreateBusRequestDto request)
        {
            var bus = new Bus()
            {
                LicenseCode = request.LicenseCode,
                Name = request.Name,
                MaxSize = request.MaxSize,
                MaxSpeed = request.MaxSpeed,
                Description = request.Description,
                Status = (Status)request.Status,
                DriverId = request.DriverId,
                MonitorId = request.MonitorId,
                RouteId = request.RouteId
            };
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return bus.Id;
        }
        public async Task<int> Update(UpdateBusRequestDto request)
        {
            var bus = await _context.Buses.Where(x=>x.IsDeleted==false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (bus == null) throw new BusTrackingException($"Can't not find any object with id is {request.Id}");
            bus.LicenseCode = request.LicenseCode;
            bus.Name = request.Name;
            bus.MaxSize = request.MaxSize;
            bus.MaxSpeed = request.MaxSpeed;
            bus.Description = request.Description;
            bus.Status = (Status)request.Status;
            bus.DriverId = request.DriverId;
            bus.MonitorId = request.MonitorId;
            bus.RouteId = request.RouteId;
            _context.Buses.Update(bus);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var bus = await _context.Buses.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == Id);
            if (bus == null)
            {
                throw new BusTrackingException($"Can't not find any object with id is {Id}");
            }
            bus.IsDeleted = true;
            _context.Buses.Update(bus);
            return await _context.SaveChangesAsync();
        }


    }
}
