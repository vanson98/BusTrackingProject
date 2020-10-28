﻿using BusTracking.Application.Catalog.StudentService;
using BusTracking.BackendApi.HubConfig;
using BusTracking.Data.Enum;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Notification;
using BusTracking.ViewModels.Catalog.Stops;
using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentTracking.BackendApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IHubContext<BusTrackingHub> _hubContext;
        public StudentController(IStudentService studentService, IHubContext<BusTrackingHub> hubContext)
        {
            _studentService = studentService;
            _hubContext = hubContext;
        }

        [Authorize(Roles ="admin")]
        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<StudentDto>> GetAllPaging([FromQuery]GetStudentPagingRequestDto request)
        {
            var result = await _studentService.GetAllPaging(request);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Get/{id}")]
        public async Task<ResultDto<StudentDto>> GetById(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
            {
                return new ResultDto<StudentDto>()
                {
                    StatusCode = ResponseCode.NotFound,
                    Message = "Không tìm thấy đối tượng",
                    Result = null
                };
            }
            return new ResultDto<StudentDto>(ResponseCode.Success, "Thực thi thành công", student);
        }


        [Authorize(Roles ="monitor")]
        [HttpGet("GetByMonitorId")]
        public async Task<ResultDto<List<StudentDto>>> GetByMonitorId(Guid monitorId)
        {
            var result = await _studentService.GetStudentByMonitorIdOrParentId(monitorId, null);
            return result;
        }

        [Authorize(Roles = "parent")]
        [HttpGet("GetByParentId")]
        public async Task<ResultDto<List<StudentDto>>> GetByParentId(Guid parentId)
        {
            var result = await _studentService.GetStudentByMonitorIdOrParentId(null,parentId);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetLogsCheckIn")]
        public async Task<PageResultDto<StudentCheckInDto>> GetLogsCheckIn([FromQuery]GetStudentCheckInRequestDto request)
        {
            var result = await _studentService.TraceLogCheckIn(request);
            return result;
        }

        [Authorize(Roles = "monitor")]
        [HttpGet("GetNotificationOfMonitor")]
        public async Task<ResultDto<List<NotificationDto>>> GetNotificationOfMonitor([FromQuery]Guid monitorId, [FromQuery]DateTime fromDate, [FromQuery]DateTime toDate)
        {
            var result = await _studentService.GetAllNotificationOfMonitor(monitorId,fromDate,toDate);
            return result;
        }

        [Authorize(Roles = "parent")]
        [HttpGet("GetNotificationOfParent")]
        public async Task<ResultDto<List<NotificationDto>>> GetNotificationOfParent([FromQuery] Guid parentId,[FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _studentService.GetAllNotificationOfParent(parentId, fromDate, toDate);
            return result;
        }

        [Authorize(Roles = "monitor,parent")]
        [HttpPost("CheckIn")]
        public async Task<ResultDto<StudentCheckInDto>> CheckIn([FromBody]CheckInRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResultDto<StudentCheckInDto>(ResponseCode.Validate, "Đầu vào không hợp lệ",null);
            }
            var res = await _studentService.CheckIn(request);
            if (res.StatusCode==ResponseCode.Success)
            {
                // Message to All
                await _hubContext.Clients.All.SendAsync("ReceiveCheckIn", res);
                var notification = await _studentService.AddNotification(res.Result);
                if (notification.TypeNotification != 7 && notification.TypeNotification != 8 )
                {
                    // Message to parent
                    await _hubContext.Clients.User(res.Result.ParentId.ToString()).SendAsync("ReceiveNotication", notification);
                }
                else
                {
                    // Message to monitor
                    await _hubContext.Clients.User(res.Result.MonitorId.ToString()).SendAsync("ReceiveNotication", notification);
                }
            }
            return res;
        }

        [Authorize(Roles = "parent")]
        [HttpPost("TakeLeave")]
        public async Task<ResponseDto> TakeLeave([FromBody] CheckInRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            if (request.CheckInResult != (int)StudentStatus.OnLeave)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var res = await _studentService.CheckIn(request);
            if (res.StatusCode == ResponseCode.Success)
            {
                // Message to web manager
                await _hubContext.Clients.All.SendAsync("ReceiveCheckIn", res);
                // Message to monitor
                var notification = await _studentService.AddNotification(res.Result);
                await _hubContext.Clients.User(res.Result.MonitorId.ToString()).SendAsync("ReceiveNotication", notification);
            }
            // Api response to Parent
            return res;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody]CreateStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var studentId = await _studentService.Create(request);
            if (studentId == 0)
            {
                return new ResponseDto(ResponseCode.LogicError, "Tạo mới không thành công");
            }
            return new ResponseDto(ResponseCode.Success, "Tạo mới thành công");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody] UpdateStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _studentService.Update(request);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần cập nhật");
            return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            int result = await _studentService.Delete(id);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Xóa không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần xóa");
            return new ResponseDto(ResponseCode.Success, "Xóa thành công");
        }
    }
}
