using BusTracking.Data.EF;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.Utilities;
using BusTracking.ViewModels.Catalog.Routes;
using BusTracking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.RouteService
{
    public class RouteService : IRouteService
    {
        private readonly BusTrackingDbContext _context;
        public RouteService(BusTrackingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> Create(CreateRouteRequestDto request)
        {
            var route = new Route()
            {
               RouteCode = request.RouteCode,
               Name = request.Name,
               Distance = request.Distance,
               Desctiption = request.Desctiption,
               Status = (Status)request.Status
            };
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
            return route.Id;
        }

        public async Task<int> Delete(int id)
        {
            var route = await _context.Routes.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (route == null)
            {
                throw new BusTrackingException($"Can't not find any object with id is {id}");
            }
            route.IsDeleted = true;
            _context.Routes.Update(route);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResultDto<RouteDto>> GetAllPaging(GetRoutePagingRequestDto request)
        {
            var query = _context.Routes.Where(x => x.IsDeleted == false).AsQueryable();
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.RouteCode))
            {
                query = query.Where(x => x.RouteCode.Contains(request.RouteCode));
            }
            if (request.Status >= 0)
            {
                query = query.Where(x => (int)x.Status == request.Status);
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new RouteDto()
                                  {
                                      Id= x.Id,
                                      Name = x.Name,
                                      Distance = x.Distance,
                                      RouteCode = x.RouteCode,
                                      Desctiption = x.Desctiption,
                                      Status = (int)x.Status
                                  }).ToListAsync();
            // Return 
            var pageResult = new PageResultDto<RouteDto>()
            {
                TotalRecord = totalRow,
                Message = "Thực hiện thành công",
                Items = data
            };
            return pageResult;
        }

        public Task<RouteDto> GetById(int busId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(UpdateRouteRequestDto request)
        {
            var route = await _context.Routes.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (route == null) throw new BusTrackingException($"Can't not find any object with id is {request.Id}");
            route.Name = request.Name;
            route.Desctiption = request.Desctiption;
            route.RouteCode = request.RouteCode;
            route.Distance = request.Distance;
            route.Status = (Status)request.Status;
            _context.Routes.Update(route);
            return await _context.SaveChangesAsync();
        }
    }
}
