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
            }else if(checkInRecord.CheckInResult == (int)StudentStatus.InClass)
            {
                notify.Content = checkInRecord.StudentName + " đã vào lớp học.";
            }else if(checkInRecord.CheckInResult == (int)StudentStatus.NotInClass)
            {
                notify.Content = checkInRecord.StudentName + " vắng mặt tại lớp học.";
            }
            else
            {
                notify.Content = "Content default";
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
            var checkInTimeSpan = new TimeSpan(studentCheckIn.CheckInTime.Hour, (studentCheckIn.CheckInTime.Minute) ,00);
            if (studentCheckIn.CheckInResult == StudentStatus.PickedUp)
            {
                TimeSpan timeCheck = checkInTimeSpan.Subtract(student.StopPickTime);
                if (timeCheck.TotalMinutes>5)
                {
                    studentCheckIn.CheckInState = CheckInState.Late;
                }
                else if(timeCheck.TotalMinutes<-5)
                {
                    studentCheckIn.CheckInState = CheckInState.Soon;
                }
                else
                {
                    studentCheckIn.CheckInState = CheckInState.OnTime;
                }
            }
            else if(studentCheckIn.CheckInResult == StudentStatus.DropedOff)
            {
                TimeSpan timeCheck = checkInTimeSpan.Subtract(student.StopDropTime);
                if (timeCheck.TotalMinutes>5)
                {
                    studentCheckIn.CheckInState = CheckInState.Late;
                    
                }
                else if (timeCheck.TotalMinutes<-5)
                {
                    studentCheckIn.CheckInState = CheckInState.Soon;
                }
                else
                {
                    studentCheckIn.CheckInState = CheckInState.OnTime;
                }
            }
            // Lưu điểm danh vào DB
            await _context.StudentCheckIns.AddAsync(studentCheckIn);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                // Cập nhật trạng thái HS
                var studentUpdateResult = await this.UpdateStatus(studentCheckIn.StudentId, (StudentStatus)request.CheckInResult);
                // Cập nhật trạng thái warning 
                var updateWarningResult = await this.UpdateWarningState(studentCheckIn.StudentId, false);
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
                Status = 0,
                ClassOfStudent = request.ClassOfStudent,
                TeacherId = request.TeacherId
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
                                      TeacherId = x.s.TeacherId,
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
                TeacherId = x.s.TeacherId,
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
        public async Task<ResultDto<List<StudentDto>>> GetStudentByUser(Guid? monitorId,Guid? parentId,Guid? teacherId)
        {
            var query = from s in _context.Students
                        where s.IsDeleted == false
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join t in _context.AppUsers on s.TeacherId equals t.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        join m in _context.AppUsers on b.MonitorId equals m.Id
                        join stp in _context.Stops on s.StopPickId equals stp.Id
                        join std in _context.Stops on s.StopDropId equals std.Id
                        select new { s, p,t, b, m, stp,std };
            if (monitorId != null)
            {
                query = query.Where(x => x.m.Id == monitorId);
            }
            if(parentId != null)
            {
                query = query.Where(x => x.s.ParentId == parentId);
            }
            if(teacherId != null)
            {
                query = query.Where(x => x.s.TeacherId == teacherId);
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
                                    TeacherId = x.t.Id,
                                    TeacherName = x.t.FullName,
                                    PhoneTeacher = x.t.PhoneNumber,
                                    StopPickId = x.s.StopPickId,
                                    StopPickAddress = x.stp.Address,
                                    StopDropAddress = x.std.Address,
                                    StopDropName = x.std.Name,
                                    StopPickName = x.stp.Name,
                                    StopDropId = x.std.Id,
                                    StopDropTime = x.std.TimeDropOff,
                                    StopPickTime = x.stp.TimePickUp,
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
                        join t in _context.AppUsers on s.TeacherId equals t.Id
                        join b in _context.Buses on s.BusId equals b.Id
                        select new { ck, m, s, p,t, b };
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
                TeacherId = x.t.Id,
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
                        where n.Type == TypeMessage.OnLeave 
                        || n.Type == TypeMessage.AtHome 
                        || n.Type == TypeMessage.WaningCheckin
                        || n.Type == TypeMessage.InClass
                        || n.Type == TypeMessage.NotInClass
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
        /// Lấy tất cả thông báo của giáo viên
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public async Task<ResultDto<List<NotificationDto>>> GetAllNotificationOfTeacher(Guid teacherId, DateTime fromDate, DateTime toDate)
        {
            var query = from s in _context.Students
                        where s.TeacherId == teacherId
                        join n in _context.Notification on s.Id equals n.StudentId
                        where n.Type == TypeMessage.OnLeave 
                        || n.Type == TypeMessage.AtHome
                        || n.Type == TypeMessage.AbsentOnDrop
                        || n.Type == TypeMessage.WaningCheckin
                        || n.Type == TypeMessage.AbsentOnPick
                        || n.Type == TypeMessage.WaningCheckin
                        where n.TimeSent.Date >= fromDate.Date && n.TimeSent.Date <= toDate.Date
                        select n;

            var notifications = await query.OrderByDescending(x => x.TimeSent).Select(x => new NotificationDto()
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
            student.TeacherId = request.TeacherId;
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
                student.WarningCheckIn = false;
                _context.Students.Update(student);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<ResultDto<TotalStudentStatus>> GetTotalStudentStatus()
        {
            var students = new TotalStudentStatus();
            students.TotalStudent = await _context.Students.CountAsync();
            students.AtSchool = await _context.Students.Where(x => x.Status == StudentStatus.AtScholl | x.Status == StudentStatus.InClass).CountAsync();
            students.Absent = await _context.Students
                                    .Where(x => x.Status == StudentStatus.AbsentOnDrop | x.Status == StudentStatus.AbsentOnPick)
                                    .CountAsync();
            students.AtHome = await _context.Students.Where(x => x.Status == StudentStatus.AtHome | x.Status == StudentStatus.Reset).CountAsync();
            students.OnBus = await _context.Students
                                    .Where(x => x.Status == StudentStatus.PickedUp | x.Status == StudentStatus.GoingHome)
                                    .CountAsync();
            students.OnLeave = await _context.Students.Where(x => x.Status == StudentStatus.OnLeave).CountAsync();
            return new ResultDto<TotalStudentStatus>()
            {
                Result = students,
                Message = "Thành công",
                StatusCode = ResponseCode.Success
            };
        }

        public async Task<ResultDto<ChartModel>> GetDataChart(int checkInType, DateTime time, int busId)
        {
            var query = from ck in _context.StudentCheckIns
                        where ck.CheckInType == (CheckInType)checkInType && ck.CheckInState != null
                        where ck.CheckInTime.Month == time.Month && ck.CheckInTime.Year == time.Year
                        join s in _context.Students on ck.StudentId equals s.Id
                        where s.BusId == busId
                        select ck;

            TotalCheckInState totalCheckInState = new TotalCheckInState();
            totalCheckInState.Soon = query.Where(x => (int)x.CheckInState == 2).Count();
            totalCheckInState.Late = query.Where(x => (int)x.CheckInState == 0).Count();
            totalCheckInState.OnTime = query.Where(x => (int)x.CheckInState == 1).Count();
            var listCheckIn = await query.Select(x => new CheckInChartModel() {
                CheckInDay = x.CheckInTime.ToString("yyyy-MM-dd"),
                CheckInState = (int)x.CheckInState
            }).ToListAsync();
            
            var chartModel = new ChartModel()
            {
                totalCheckInState = totalCheckInState,
                checkInChartModel = listCheckIn
            };
            return new ResultDto<ChartModel>()
            {
                Message = "Thành công",
                Result = chartModel,
                StatusCode = ResponseCode.Success
            };
        }

        public async Task<List<WarningCheckInDto>> CheckStudentStatus()
        {
            var listWarning = new List<WarningCheckInDto>();
            DateTime now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute,00);
            var query = from s in _context.Students  where s.IsDeleted == false
                        join b in _context.Buses on s.BusId equals b.Id
                        join m in _context.AppUsers on b.MonitorId equals m.Id
                        join p in _context.AppUsers on s.ParentId equals p.Id
                        join t in _context.AppUsers on s.TeacherId equals t.Id
                        join stp in _context.Stops on s.StopPickId equals stp.Id
                        join std in _context.Stops on s.StopDropId equals std.Id
                        select new { s, b, m, p,t, stp, std };
            // Lấy danh sách học sinh
            var listStudent = query.Where(x=>x.s.WarningCheckIn==false).ToList();
            foreach (var student in listStudent)
            {
                var warning = new WarningCheckInDto();
                if (student.s.Status == StudentStatus.Reset)
                {
                    if (nowTimeSpan.Subtract(student.stp.TimePickUp).TotalMinutes > 30)
                    {
                        warning.Content = $"Đã 30 phút trôi qua, {student.s.Name} vẫn chưa được xác nhận được đón.";
                        
                    }
                }
                else if(student.s.Status == StudentStatus.PickedUp)
                {
                    if (nowTimeSpan.Subtract(new TimeSpan(7,00,00)).TotalMinutes > 30)
                    {
                        warning.Content = $"Đã 30 phút trôi qua, {student.s.Name} vẫn chưa được xác nhận đã tới trường.";
                       
                    }
                }
                else if(student.s.Status == StudentStatus.GoingHome)
                {
                    if (nowTimeSpan.Subtract(student.std.TimeDropOff).TotalMinutes > 30)
                    {
                        warning.Content = $"Đã 30 phút trôi qua, {student.s.Name} vẫn chưa được xác nhận đã được trả.";
                       
                    }
                } 
                else if (student.s.Status == StudentStatus.DropedOff)
                {
                    if (nowTimeSpan.Subtract(new TimeSpan(18,30,00)).TotalMinutes > 30)
                    {
                        warning.Content = $"Đã 30 phút trôi qua, {student.s.Name} vẫn chưa được xác nhận đã về nhà.";
                        
                    }
                }
                else if (student.s.Status == StudentStatus.AtScholl)
                {
                    if (nowTimeSpan.Subtract(new TimeSpan(7, 30, 00)).TotalMinutes > 30)
                    {
                        warning.Content = $"Đã 30 phút trôi qua, {student.s.Name} vẫn chưa được xác nhận đã ở trong lớp học.";
                    }
                }
                warning.TimeSent = now;
                warning.TypeNotification = 11;
                warning.MonitorId = student.m.Id.ToString();
                warning.ParentId = student.p.Id.ToString();
                warning.TeacherId = student.t.Id.ToString();
                if (warning.Content != null)
                {
                    var addNotifyResult = await this.AddWarning(student.s.Id, 11, warning.Content, now);
                    var updateWarningResult = await this.UpdateWarningState(student.s.Id, true);
                    if (addNotifyResult != -1 && updateWarningResult!=0)
                    {
                        warning.Id = addNotifyResult;
                        listWarning.Add(warning);
                    }
                }
            }
            return listWarning;
        }

        // Lưu cảnh báo vào DB
        public async Task<int> AddWarning(int studentId, int type, string content, DateTime timeSent)
        {
            var notification = new Notification() { 
                StudentId = studentId,
                Content = content,
                TimeSent = timeSent,
                Type = (TypeMessage)type
            };
            await _context.Notification.AddAsync(notification);
            var result =  await _context.SaveChangesAsync();
            if (result > 0)
            {
                return notification.Id;
            }
            return -1;
        }

        // Cập nhật trạng thái cảnh báo cho học sinh
        public async Task<int> UpdateWarningState(int studentId, bool isWarning)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x=>x.Id==studentId);
            student.WarningCheckIn = isWarning;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }
    }
}
