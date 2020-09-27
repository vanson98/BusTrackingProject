using BusTracking.Data.EF;
using BusTracking.ViewModels.Catalog.Drivers;
using BusTracking.ViewModels.Common;
using System;
using System.Threading.Tasks;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.Utilities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using BusTracking.Utilities.Constants;

namespace BusTracking.Application.Catalog.DriverService
{
    public class DriverService : IDriverService
    {
        private readonly BusTrackingDbContext _context;
        public DriverService(BusTrackingDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<int> CreateAsync(CreateDriverRequestDto requestDto)
        {
            // Xử lý try catch 
            var driver = new Driver() { 
                Address = requestDto.Address,
                Dob = requestDto.Dob,
                Email = requestDto.Email,
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber,
                Status = (Status)requestDto.Status
            };
            await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            return driver.Id;
        }

        public async Task<int> DeleteAsync(int Id)
        {
            
            var driver = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == Id);
            if (driver == null)
            {
                return -1;
            }
            driver.IsDeleted = true;
            _context.Drivers.Update(driver);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<DriverDto>> GetAllDriverUnAssign()
        {
            var query = from dr in _context.Drivers
                        join b in _context.Buses on dr.Id equals b.DriverId into gb
                        from sub in gb.DefaultIfEmpty() where sub.DriverId == null
                        select dr;
            var bus = await query.Select(x=>new DriverDto() {
                Id = x.Id,
                Address = x.Address,
                Dob = x.Dob,
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Status = (int)x.Status
            }).ToListAsync();
            return bus;
        }

        public async Task<PageResultDto<DriverDto>> GetAllPagingAsync(GetDriverPagingRequestDto request)
        {
            var query = _context.Drivers.Where(x=>x.IsDeleted==false).AsQueryable();
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(request.PhoneNumber));
            }
            if (request.Status>=0)
            {
                query = query.Where(x => (int)x.Status == request.Status);
            }
            // Paging
            var totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new DriverDto()
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Address = x.Address,
                                      Dob = x.Dob,
                                      Email = x.Email,
                                      PhoneNumber = x.PhoneNumber,
                                      Status = (int)x.Status
                                  }).ToListAsync();
            var pageResult = new PageResultDto<DriverDto>()
            {
                StatusCode = ResponseCode.Success,
                TotalRecord = totalRow,
                Message = "Thực hiện thành công",
                Items = data
            };
            return pageResult;
        }

        public async Task<DriverDto> GetByIdAsync(int Id)
        {
            var x = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(d=>d.Id==Id);
            if (x == null)
                return null;
            var driver = new DriverDto()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Dob = x.Dob,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Status = (int)x.Status
            };
            return driver;
        }

        public async Task<int> UpdateAsync(UpdateDriverRequestDto request)
        {
            var driver = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (driver == null) return -1;
            driver.Name = request.Name;
            driver.Dob = request.Dob;
            driver.Address = request.Address;
            driver.Email = request.Email;
            driver.PhoneNumber = request.PhoneNumber;
            driver.Status = (Status)request.Status;
            _context.Drivers.Update(driver);
            return await _context.SaveChangesAsync();
        }
    }
}
