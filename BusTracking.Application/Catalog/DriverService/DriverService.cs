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

namespace BusTracking.Application.Catalog.DriverService
{
    public class DriverService : IDriverService
    {
        private readonly BusTrackingDbContext _context;
        public DriverService(BusTrackingDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<int> Create(CreateDriverRequestDto requestDto)
        {
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

        public async Task<int> Delete(int Id)
        {
            var driver = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == Id);
            if (driver == null)
            {
                throw new BusTrackingException($"Can't not find any object with id is {Id}");
            }
            driver.IsDeleted = true;
            _context.Drivers.Update(driver);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResultDto<DriverDto>> GetAllPaging(GetDriverPagingRequestDto request)
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
            int totalRow = await query.CountAsync();
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
            // Return 
            var pageResult = new PageResultDto<DriverDto>()
            {
                TotalRecord = totalRow,
                Message = "Thực hiện thành công",
                Items = data
            };
            return pageResult;
        }

        public async Task<DriverDto> GetById(int Id)
        {
            var x = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(d=>d.Id==Id);
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

        public async Task<int> Update(UpdateDriverRequestDto request)
        {
            var driver = await _context.Drivers.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (driver == null) throw new BusTrackingException($"Can't not find any object with id is {request.Id}");
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
