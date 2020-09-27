﻿using BusTracking.Data.EF;
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
            var query = _context.Stops.Where(x => x.IsDeleted == false).AsQueryable();
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(x => x.Address.Contains(request.Address));
            }
            if (request.Status >= 0)
            {
                query = query.Where(x => (int)x.Status == request.Status);
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new StopDto()
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
                Status = (Status)request.Status
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
            _context.Stops.Update(stop);
            return await _context.SaveChangesAsync();
        }
    }
}