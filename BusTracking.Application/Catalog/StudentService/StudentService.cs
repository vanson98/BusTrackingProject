using BusTracking.Data.EF;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.Utilities;
using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly BusTrackingDbContext _context;
        public StudentService(BusTrackingDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<int> Create(CreateStudentRequestDto request)
        {
            var student = new Student()
            {
                BusId = request.BusId,
                ParentId = request.ParentId,
                Name = request.Name,
                Address = request.Address,
                Dob = request.Dob,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = (StudentStatus)request.Status
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student.Id;
        }

        public async Task<int> Delete(int id)
        {
            var student = await _context.Students.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                throw new BusTrackingException($"Can't not find any object with id is {id}");
            }
            student.IsDeleted = true;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResultDto<StudentDto>> GetAllPaging(GetStudentPagingRequestDto request)
        {
            //var query = _context.Students.Where(x => x.IsDeleted == false).AsQueryable();
            var query = from s in _context.Students
                        where s.IsDeleted == false
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { s, p, b };
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.s.Name == request.Name);
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(x => x.s.PhoneNumber == request.PhoneNumber);
            }
            if (request.ParentId != null)
            {
                query = query.Where(x => x.s.ParentId == request.ParentId);
            }
            if (request.BusId > 0)
            {
                query = query.Where(x => x.s.BusId == request.BusId);
            }
            // Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new StudentDto()
                                  {
                                      Id = x.s.Id,
                                      BusId = x.s.BusId,
                                      BusName = x.b.Name,
                                      ParentId = x.p.Id,
                                      ParentName = x.p.FullName,
                                      Name = x.s.Name,
                                      Address = x.s.Address,
                                      Dob = x.s.Dob,
                                      Email = x.s.Email,
                                      PhoneNumber = x.s.PhoneNumber,
                                      Status = (int)x.s.Status
                                  }).ToListAsync();
            // Return 
            var pageResult = new PageResultDto<StudentDto>()
            {
                TotalRecord = totalRow,
                Message = "Thực hiện thành công",
                Items = data
            };
            return pageResult;
        }

        public async Task<StudentDto> GetById(int id)
        {
            // Select and Join 
            var query = from s in _context.Students
                        where s.Id == id && s.IsDeleted == false
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { s, p, b };
            var x = await query.FirstOrDefaultAsync();
            if (x == null) throw new BusTrackingException($"Can't not find any object with id is {id}");
            return new StudentDto()
            {
                Id = x.s.Id,
                BusId = x.s.BusId,
                BusName = x.b.Name,
                ParentId = x.p.Id,
                ParentName = x.p.FullName,
                Name = x.s.Name,
                Address = x.s.Address,
                Dob = x.s.Dob,
                Email = x.s.Email,
                PhoneNumber = x.s.PhoneNumber,
                Status = (int)x.s.Status
            };
        }

        public async Task<int> Update(UpdateStudentRequestDto request)
        {
            var student = await _context.Students.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (student == null) throw new BusTrackingException($"Can't not find any object with id is {request.Id}");
            student.BusId = request.BusId;
            student.ParentId = request.ParentId;
            student.Name = request.Name;
            student.Address = request.Address;
            student.Dob = request.Dob;
            student.Email = request.Email;
            student.PhoneNumber = request.PhoneNumber;
            student.Status = (StudentStatus)request.Status;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }
    }
}
