using BusTracking.Data.EF;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.Utilities;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Stops;
using BusTracking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.StopService
{
    public class StopService : IStopService
    {
        private readonly BusTrackingDbContext _context;
        public StopService(BusTrackingDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<PageResultDto<StopDto>> GetAllPaging(GetStopPagingReqestDto request)
        {
            var query = from s in _context.Stops
                        where s.IsDeleted == false
                        join r in _context.Routes on s.RouteId equals r.Id
                        select new { s, r };
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.s.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(x => x.s.Address.Contains(request.Address));
            }
            if (request.Status >= 0)
            {
                query = query.Where(x => (int)x.s.Status == request.Status);
            }
            if (request.RouteId != null)
            {
                query = query.Where(x => x.s.RouteId == request.RouteId);
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new StopDto()
                                  {
                                      Id = x.s.Id,
                                      Name = x.s.Name,
                                      Address = x.s.Address,
                                      Longitude = x.s.Longitude,
                                      Latitude = x.s.Latitude,
                                      TimePickUp = x.s.TimePickUp,
                                      TimeDropOff = x.s.TimeDropOff,
                                      NumberOfStudents = x.s.NumberOfStudents,
                                      Status = (int)x.s.Status,
                                      TypeStop = (int)x.s.TypeStop,
                                      RouteId = x.s.RouteId,
                                      RouteName = x.r.Name
                                  }).ToListAsync();
            // Return 
            var pageResult = new PageResultDto<StopDto>()
            {
                StatusCode= ResponseCode.Success,
                Message = "Thành công",
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
        public async Task<StopDto> GetById(int id)
        {
            var x = await _context.Stops.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (x == null)
                return null;
            var stop = new StopDto()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Longitude = x.Longitude,
                Latitude = x.Latitude,
                TimePickUp = x.TimePickUp,
                TimeDropOff = x.TimeDropOff,
                NumberOfStudents = x.NumberOfStudents,
                Status = (int)x.Status
            };
            return stop;
        }
        public async Task<ResultDto<List<StopMapDto>>> GetAllByMonitor(Guid monitorId, int typeStop)
        {
            var query = from s in _context.Stops
                        where s.IsDeleted == false && s.TypeStop == (TypeStop)typeStop
                        join r in _context.Routes on s.RouteId equals r.Id
                        join b in _context.Buses on r.Id equals b.RouteId
                        join m in _context.AppUsers on b.MonitorId equals m.Id
                        select new { s, r, m };
            // Filter
            if (monitorId!=null)
            {
                query = query.Where(x => x.m.Id == monitorId);
            }
            if (typeStop>=0)
            {
                query = query.Where(x => x.s.TypeStop == (TypeStop)typeStop);
            }
           
            // Paging
            var data = await query.Select(x => new StopMapDto()
                                  {
                                      Id = x.s.Id,
                                      Name = x.s.Name,
                                      Address = x.s.Address,
                                      Coordinate = new Coordinate() {Latitude=x.s.Latitude,Longitude=x.s.Longitude },
                                      TimePickUp = x.s.TimePickUp.Hours.ToString() +":"+ x.s.TimePickUp.Minutes.ToString(),
                                      TimeDropOff = x.s.TimeDropOff.Hours.ToString() + ":" + x.s.TimeDropOff.Minutes.ToString(),
                                      NumberOfStudents = x.s.NumberOfStudents
                                  }).ToListAsync();
            // Return 
            var result = new ResultDto<List<StopMapDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = data
            };
            return result;
        }
        public async Task<int> Create(CreateStopRequestDto request)
        {
            var stop = new Stop()
            {
                Name = request.Name,
                Address = request.Address,
                NumberOfStudents = request.NumberOfStudents,
                TimePickUp = new TimeSpan(request.HourPickUp, request.MinutePickUp, 0),
                TimeDropOff = new TimeSpan(request.HourDropOff, request.MinuteDropOff, 0),
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Status = (Status)request.Status,
                TypeStop = (TypeStop)request.TypeStop,
                RouteId = request.RouteId
            };
            await _context.Stops.AddAsync(stop);
            await _context.SaveChangesAsync();
            return stop.Id;
        }

        public async Task<int> Delete(int id)
        {
            var stop = await _context.Stops.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (stop == null)
            {
                throw new BusTrackingException($"Can't not find any object with id is {id}");
            }
            stop.IsDeleted = true;
            _context.Stops.Update(stop);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(UpdateStopRequestDto request)
        {
            var stop = await _context.Stops.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (stop == null) return -1;
            stop.Name = request.Name;
            stop.Address = request.Address;
            stop.NumberOfStudents = request.NumberOfStudents;
            stop.Longitude = request.Longitude;
            stop.Latitude = request.Latitude;
            stop.TimePickUp = new TimeSpan(request.HourPickUp, request.MinutePickUp, 0);
            stop.TimeDropOff = new TimeSpan(request.HourDropOff, request.MinuteDropOff, 0);
            stop.Status = (Status)request.Status;
            stop.TypeStop = (TypeStop)request.TypeStop;
            stop.RouteId = request.RouteId;
            _context.Stops.Update(stop);
            return await _context.SaveChangesAsync();
        }

        public async Task<ResultDto<List<StopDto>>> GetAllByType(int typeStop)
        {
            var query = from s in _context.Stops
                        where s.IsDeleted == false && s.TypeStop == (TypeStop)typeStop
                        select s;
            
            if (typeStop >= 0)
            {
                query = query.Where(x => x.TypeStop == (TypeStop)typeStop);
            }

            // Paging
            var data = await query.Select(x => new StopDto()
                                  {
                                      Id = x.Id,
                                      Name = x.Name
                                  }).ToListAsync();
            // Return 
            var result = new ResultDto<List<StopDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = data
            };
            return result;
        }
    }
}
