using BusTracking.Data.EF;
using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.Utilities;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Notification;
using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
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

        /// <summary>
        /// Add thông báo vào DB đồng thời trả về bản ghi thông báo
        /// </summary>
        /// <param name="checkInRecord"></param>
        /// <returns></returns>
        public async Task<NotificationDto> AddNotification(StudentCheckInDto checkInRecord)
        {
            var notify = new Notification()
            {
                TimeSent = checkInRecord.CheckInTime,
                StudentId = checkInRecord.StudentId,
                Type = (TypeMessage)checkInRecord.CheckInResult
            };
            if (checkInRecord.CheckInResult == (int)StudentStatus.AbsentOnPick)
            {
                notify.Content = checkInRecord.StudentName + " không có mặt tại điểm đón";
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.PickedUp)
            {
                notify.Content = checkInRecord.StudentName + " đã lên xe";
                if (checkInRecord.CheckInState == (int)CheckInState.Late)
                {
                    notify.Content += " (đón muộn)";
                }
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.AtScholl)
            {
                notify.Content = checkInRecord.StudentName + " đã tới trường";
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.AbsentOnDrop)
            {
                notify.Content = checkInRecord.StudentName + " vắng mặt lúc về";
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.GoingHome)
            {
                notify.Content = checkInRecord.StudentName + " đang trên xe về, hãy chuẩn bị đón con!";
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.DropedOff)
            {
                notify.Content = checkInRecord.StudentName + " đã xuống xe, hãy ra đón con!";
                if (checkInRecord.CheckInState == (int)CheckInState.Late)
                {
                    notify.Content += " (trả muộn)";
                }
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.OnLeave)
            {
                notify.Content = checkInRecord.StudentName + " đã được xin nghỉ học";
            }
            else if (checkInRecord.CheckInResult == (int)StudentStatus.AtHome)
            {
                notify.Content = checkInRecord.StudentName + " đã về nhà";
            }
            await _context.Notification.AddAsync(notify);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var notificationDto = new NotificationDto()
                {
                    Id = notify.Id,
                    Content = notify.Content,
                    ParentId = checkInRecord.ParentId,
                    MonitorId = checkInRecord.MonitorId,
                    StudentId = notify.StudentId,
                    TimeSent = notify.TimeSent,
                    TypeNotification = (int)notify.Type
                };
                return notificationDto;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Điểm danh học sinh lúc đi và lúc về (Cho app của GSX)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResultDto<StudentCheckInDto>> CheckIn(CheckInRequestDto request)
        {
            var studentCheckIn = new StudentCheckIn()
            {
                CheckInResult = (StudentStatus)request.CheckInResult,
                CheckInTime = request.CheckInTime,
                CheckInType = (CheckInType)request.CheckInType,
                MonitorId = request.MonitorId,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                StudentId = request.StudentId
            };
            var student = await this.GetById(request.StudentId);
            var checkInTimeSpan = new TimeSpan(studentCheckIn.CheckInTime.Hour, studentCheckIn.CheckInTime.Minute,00);
            if(studentCheckIn.CheckInType == CheckInType.PickUp)
            {
                if (checkInTimeSpan < student.StopPickTime)
                {
                    studentCheckIn.CheckInState = CheckInState.Soon;
                }
                else if(checkInTimeSpan == student.StopPickTime)
                {
                    studentCheckIn.CheckInState = CheckInState.OnTime;
                }
                else
                {
                    studentCheckIn.CheckInState = CheckInState.Late;
                }
            }
            else if(studentCheckIn.CheckInType == CheckInType.DropOff)
            {
                if (checkInTimeSpan < student.StopDropTime)
                {
                    studentCheckIn.CheckInState = CheckInState.Soon;
                }
                else if (checkInTimeSpan == student.StopDropTime)
                {
                    studentCheckIn.CheckInState = CheckInState.OnTime;
                }
                else
                {
                    studentCheckIn.CheckInState = CheckInState.Late;
                }
            }
            // Lưu điểm danh vào DB
            await _context.StudentCheckIns.AddAsync(studentCheckIn);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                // Cập nhật trạng thái HS
                var studentUpdateResult = await this.UpdateStatus(studentCheckIn.StudentId, (StudentStatus)request.CheckInResult);
                // Lấy StudentCheckInDto
                var itemCheckIn = await this.GetItemCheckInById(studentCheckIn.Id);
                if (studentUpdateResult != 0 && itemCheckIn != null)
                {
                    return new ResultDto<StudentCheckInDto>(ResponseCode.Success, "Điểm danh thành công", itemCheckIn);
                }
                return new ResultDto<StudentCheckInDto>(ResponseCode.LogicError, "Điểm danh thất bại", null);
            }
            else
            {
                return new ResultDto<StudentCheckInDto>(ResponseCode.LogicError, "Điểm danh thất bại", null);
            }
        }


        /// <summary>
        /// Thêm mới HS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> Create(CreateStudentRequestDto request)
        {
            var student = new Student()
            {
                BusId = request.BusId,
                ParentId = request.ParentId,
                StopPickId = request.StopPickId,
                StopDropId = request.StopDropId,
                Name = request.Name,
                Address = request.Address,
                Dob = request.Dob,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = null,
                ClassOfStudent = request.ClassOfStudent,
                TeacherName = request.TeacherName,
                PhoneTeacher = request.PhoneTeacher
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student.Id;
        }

        /// <summary>
        /// Xóa HS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(int id)
        {
            var student = await _context.Students.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return -1;
            }
            student.IsDeleted = true;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Tìm kiếm và phân trang danh sách học sinh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResultDto<StudentDto>> GetAllPaging(GetStudentPagingRequestDto request)
        {
            var query = from s in _context.Students
                        where s.IsDeleted == false
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        join stp in _context.Stops on s.StopPickId equals stp.Id
                        join std in _context.Stops on s.StopDropId equals std.Id 
                        select new { s, p, b,stp,std };
            // Filter
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.s.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.StopName))
            {
                query = query.Where(x => x.stp.Name.Contains(request.StopName));
            }
            if (!string.IsNullOrEmpty(request.BusName))
            {
                query = query.Where(x => x.b.Name.Contains(request.BusName));
            }
            if (request.StudentStatus != null)
            {
                query = query.Where(x => x.s.Status == (StudentStatus)request.StudentStatus);
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
                                      ClassOfStudent = x.s.ClassOfStudent,
                                      TeacherName = x.s.TeacherName,
                                      PhoneTeacher = x.s.PhoneTeacher,
                                      StopPickId = x.stp.Id,
                                      StopDropId = x.std.Id,
                                      StopPickName = x.stp.Name,
                                      StopDropName = x.std.Name,
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
                StatusCode = ResponseCode.Success,
                Message = "Thực hiện thành công",
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        /// <summary>
        /// Lấy Student theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentDto> GetById(int id)
        {
            // Select and Join 
            var query = from s in _context.Students
                        where s.Id == id && s.IsDeleted == false
                        join stp in _context.Stops on s.StopPickId equals stp.Id
                        join std in _context.Stops on s.StopDropId equals std.Id
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { s, p, b, stp, std };
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
                ClassOfStudent = x.s.ClassOfStudent,
                TeacherName = x.s.TeacherName,
                PhoneTeacher = x.s.PhoneTeacher,
                StopPickTime = x.stp.TimePickUp,
                StopDropTime = x.std.TimeDropOff,
                Email = x.s.Email,
                PhoneNumber = x.s.PhoneNumber,
                Status = (int)x.s.Status
            };
        }

        /// <summary>
        /// Lấy tất cả danh sách học sinh của GSX hoặc Phụ huynh, tùy theo ID truyền vào
        /// </summary>
        /// <param name="monitorId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<StudentDto>>> GetStudentByMonitorIdOrParentId(Guid? monitorId,Guid? parentId)
        {
            var query = from s in _context.Students
                        where s.IsDeleted == false
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        join m in _context.AppUsers on b.MonitorId equals m.Id
                        join stp in _context.Stops on s.StopPickId equals stp.Id
                        join std in _context.Stops on s.StopDropId equals std.Id
                        select new { s, p, b, m, stp,std };
            if (monitorId != null)
            {
                query = query.Where(x => x.m.Id == monitorId);
            }
            if(parentId != null)
            {
                query = query.Where(x => x.s.ParentId == parentId);
            }
            var listStudent = await query.Select(x=>new StudentDto() 
                                 { 
                                    Id = x.s.Id,
                                    Name = x.s.Name,
                                    Dob = x.s.Dob,
                                    Address = x.s.Address,
                                    Email = x.s.Email,
                                    PhoneNumber = x.s.PhoneNumber,
                                    Status = (int)x.s.Status,
                                    BusId = x.b.Id,
                                    BusName = x.b.Name,
                                    MonitorId = x.m.Id,
                                    MonitorName = x.m.FullName,
                                    PhoneMonitor = x.m.PhoneNumber,
                                    ParentId = x.p.Id,
                                    ParentName = x.p.FullName,
                                    PhoneParent = x.p.PhoneNumber,
                                    TeacherName = x.s.TeacherName,
                                    PhoneTeacher = x.s.PhoneTeacher,
                                    StopPickId = x.s.StopPickId,
                                    StopPickAddress = x.stp.Address,
                                    StopDropAddress = x.std.Address,
                                    StopDropName = x.std.Name,
                                    StopPickName = x.stp.Name,
                                    ClassOfStudent = x.s.ClassOfStudent
                                })
                                .ToListAsync();

            return new ResultDto<List<StudentDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = listStudent
            };
        }

        /// <summary>
        /// Lấy ra record check in 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentCheckInDto> GetItemCheckInById(int id)
        {
            var query = from ck in _context.StudentCheckIns
                        from m in _context.AppUsers.Where(m => m.Id == ck.MonitorId).DefaultIfEmpty()
                        join s in _context.Students on ck.StudentId equals s.Id
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { ck, m, s, p, b };
            var checkInItem = await query.Where(x => x.ck.Id == id).Select(x => new StudentCheckInDto()
            {
                Id = x.ck.Id,
                BusName = x.b.Name,
                CheckInResult = (int)x.ck.CheckInResult,
                CheckInTime = x.ck.CheckInTime,
                CheckInType = (int)x.ck.CheckInType,
                MonitorId = x.m == null ? Guid.Empty : x.m.Id,
                MonitorName = x.m == null ? null : x.m.FullName,
                StudentName = x.s.Name,
                StudentId = x.s.Id,
                ParentId = x.p.Id,
                Latitude = x.ck.Latitude,
                Longitude = x.ck.Longitude,
                CheckInState = (int)x.ck.CheckInState
            }).FirstOrDefaultAsync();
            return checkInItem;
        }

        /// <summary>
        /// Lấy tất cả message của  giám sát xe
        /// </summary>
        /// <param name="id">UserId của  GSX</param>
        /// <param name="type">Loại thông báo</param>
        /// <returns></returns>
        public async Task<ResultDto<List<NotificationDto>>> GetAllNotificationOfMonitor(Guid id, DateTime fromDate, DateTime toDate)
        {
            var query = from b in _context.Buses
                        where b.MonitorId == id
                        join s in _context.Students on b.Id equals s.BusId
                        join n in _context.Notification on s.Id equals n.StudentId
                        where n.Type == TypeMessage.OnLeave | n.Type == TypeMessage.AtHome
                        where n.TimeSent.Date >= fromDate.Date && n.TimeSent.Date <= toDate.Date
                        select n;

            var notifications = await query.OrderByDescending(x=>x.TimeSent).Select(x => new NotificationDto()
            {
                Id = x.Id,
                TypeNotification = (int)x.Type,
                Content = x.Content,
                TimeSent = x.TimeSent
            }).ToListAsync();
            return new ResultDto<List<NotificationDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = notifications
            };
        }

        /// <summary>
        /// Lấy tất cả notification của phụ huynh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<NotificationDto>>> GetAllNotificationOfParent(Guid parentId, DateTime fromDate, DateTime toDate)
        {
            var query = from s in _context.Students
                        where s.ParentId == parentId
                        join n in _context.Notification on s.Id equals n.StudentId
                        where n.Type != TypeMessage.OnLeave && n.Type != TypeMessage.AtHome
                        where n.TimeSent.Date >= fromDate.Date && n.TimeSent.Date <= toDate.Date
                        select n;
            
            var notifications = await query.OrderByDescending(x=>x.TimeSent).Select(x => new NotificationDto()
            {
                Id = x.Id,
                TypeNotification = (int)x.Type,
                Content = x.Content,
                TimeSent = x.TimeSent
            }).ToListAsync();
            return new ResultDto<List<NotificationDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = notifications
            };
        }

        /// <summary>
        /// Tra cứu lịch sử điểm danh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResultDto<StudentCheckInDto>> TraceLogCheckIn(GetStudentCheckInRequestDto request)
        {
            var query = from ck in _context.StudentCheckIns
                        from m in _context.AppUsers.Where(m => m.Id == ck.MonitorId).DefaultIfEmpty()
                        join s in _context.Students on ck.StudentId equals s.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { ck,m,s,b };
            if (request.BusId != null)
            {
                query = query.Where(x => x.b.Id == request.BusId);
            }
            if (request.StudentName != null)
            {
                query = query.Where(x => x.s.Name.Contains(request.StudentName));
            }
            if (request.FromDate != null)
            {
                query = query.Where(x => x.ck.CheckInTime > request.FromDate);
            }
            if (request.ToDate != null)
            {
                query = query.Where(x => x.ck.CheckInTime < request.ToDate);
            }
            if (request.CheckInType != null)
            {
                query = query.Where(x => x.ck.CheckInType == (CheckInType)request.CheckInType);
            }
            if(request.CheckInResult != null)
            {
                query = query.Where(x => x.ck.CheckInResult == (StudentStatus)request.CheckInResult);
            }
            var listStudent = await query.OrderByDescending(x=>x.ck.CheckInTime).Select(x => new StudentCheckInDto()
            {
                Id = x.ck.Id,
                BusName = x.b.Name,
                CheckInResult = (int)x.ck.CheckInResult,
                CheckInTime = x.ck.CheckInTime,
                CheckInType = (int)x.ck.CheckInType,
                MonitorName = x.m.FullName,
                StudentName = x.s.Name,
                StudentId = x.s.Id,
                Longitude = x.ck.Longitude,
                Latitude = x.ck.Latitude
            }).ToListAsync();

            return new PageResultDto<StudentCheckInDto>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Items = listStudent
            };
        }

        /// <summary>
        /// Cập nhật HS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> Update(UpdateStudentRequestDto request)
        {
            var student = await _context.Students.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (student == null) return -1;
            student.BusId = request.BusId;
            student.ParentId = request.ParentId;
            student.StopPickId = request.StopPickId;
            student.StopDropId = request.StopDropId;
            student.Name = request.Name;
            student.Address = request.Address;
            student.Dob = request.Dob;
            student.Email = request.Email;
            student.PhoneNumber = request.PhoneNumber;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật trạng thái học sinh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(int id, StudentStatus status)
        {
            var student = await _context.Students.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            student.Status = status;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Reset trạng thái học sinh về mặc định
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<int> ResetStatus(int status)
        {
            var students = await _context.Students
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Status != StudentStatus.AbsentOnPick)
                .Where(x => x.Status != StudentStatus.AbsentOnDrop)
                .ToListAsync();
            foreach (var student in students)
            {
                student.Status = (StudentStatus)status;
                _context.Students.Update(student);
            }
            return await _context.SaveChangesAsync();
        }

    }
}
