using BusTracking.Application.Catalog.DriverService;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Drivers;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverService _driverService { get; set; }
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("GetAllDriverUnAssign")]
        public async Task<ResultDto<List<DriverDto>>> GetAllDriverUnAssignAsync()
        {
            var drivers = await this._driverService.GetAllDriverUnAssign();
            return new ResultDto<List<DriverDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thực hiện thành công",
                Result = drivers
            };
        }

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<DriverDto>> GetAllPaging([FromQuery]GetDriverPagingRequestDto request)
        {
            var result = await _driverService.GetAllPagingAsync(request);
            return result;
        }

        [HttpGet("Get")]
        public async Task<ResultDto<DriverDto>> GetById([FromQuery]int id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            if (driver == null)
            {
                return new ResultDto<DriverDto>()
                {
                    StatusCode = ResponseCode.NotFound,
                    Message = "Không tìm thấy đối tượng",
                    Result = null
                };
            }
            return new ResultDto<DriverDto>(ResponseCode.Success, "Thực thi thành công", driver);
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody]CreateDriverRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var driverId = await _driverService.CreateAsync(request);
            if (driverId == 0)
            {
                return new ResponseDto(ResponseCode.LogicError, "Tạo mới không thành công");
            }
            return new ResponseDto(ResponseCode.Success, "Tạo mới thành công");
        }

        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody]UpdateDriverRequestDto request)
        {
            if (!ModelState.IsValid) 
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _driverService.UpdateAsync(request);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật không thành công");
            if(result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần cập nhật");
            return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            int result = await _driverService.DeleteAsync(id);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Xóa không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần xóa");
            return new ResponseDto(ResponseCode.Success, "Xóa thành công");
        }
    }
}
