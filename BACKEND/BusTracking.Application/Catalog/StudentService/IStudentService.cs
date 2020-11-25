using BusTracking.Data.Entities;
using BusTracking.Data.Enum;
using BusTracking.ViewModels.Catalog.Notification;
using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.StudentService
{
    public interface IStudentService
    {
        Task<PageResultDto<StudentDto>> GetAllPaging(GetStudentPagingRequestDto request);
        Task<StudentDto> GetById(int busId);
        Task<ResultDto<List<StudentDto>>> GetStudentByMonitorIdOrParentId(Guid? monitorId, Guid? parentId);
        Task<int> Create(CreateStudentRequestDto requestDto);
        Task<int> Update(UpdateStudentRequestDto requestDto);
        Task<int> Delete(int studentId);
        Task<PageResultDto<StudentCheckInDto>> TraceLogCheckIn(GetStudentCheckInRequestDto request);
        Task<ResultDto<StudentCheckInDto>> CheckIn(CheckInRequestDto request);
        Task<int> UpdateStatus(int studentId, StudentStatus status);
        Task<int> ResetStatus(int status);
        Task<StudentCheckInDto> GetItemCheckInById(int id);
        Task<NotificationDto> AddNotification(StudentCheckInDto studentCheckIn);
        Task<ResultDto<List<NotificationDto>>> GetAllNotificationOfMonitor(Guid id,DateTime fromDate,DateTime toDate);
        Task<ResultDto<List<NotificationDto>>> GetAllNotificationOfParent(Guid id,DateTime fromDate,DateTime toDate);
        Task<ResultDto<TotalStudentStatus>> GetTotalStudentStatus();
        Task<ResultDto<ChartModel>> GetDataChart(int checkInType,DateTime time, int busId);

    }
}
