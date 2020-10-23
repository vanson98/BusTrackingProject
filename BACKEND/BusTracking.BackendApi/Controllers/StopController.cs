using BusTracking.Application.Catalog.StopService;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Stops;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StopTracking.BackendApi.Controllers
{
    [Authorize(Roles ="admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class StopController : ControllerBase
    {
        private readonly IStopService _stopService;
        public StopController(IStopService stopService)
        {
            _stopService = stopService;
        }

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<StopDto>> GetAllPaging([FromQuery]GetStopPagingReqestDto request)
        {
            var result = await _stopService.GetAllPaging(request);
            return result;
        }

        [HttpGet("Get/{id}")]
        public async Task<ResultDto<StopDto>> GetById(int id)
        {
            var stop = await _stopService.GetById(id);
            if (stop == null)
                return new ResultDto<StopDto>() { 
                    StatusCode = ResponseCode.NotFound, 
                    Message = "Không tìm thấy đối tượng", 
                    Result = null 
                };
            return new ResultDto<StopDto>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thành công",
                Result = stop
            };
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody]CreateStopRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var stopId = await _stopService.Create(request);
            if (stopId == 0)
            {
                return new ResponseDto(ResponseCode.LogicError, "Tạo mới không thành công");
            }
            return new ResponseDto(ResponseCode.Success, "Tạo mới thành công");
        }

        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody] UpdateStopRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _stopService.Update(request);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần cập nhật");
            return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            int result = await _stopService.Delete(id);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Xóa không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần xóa");
            return new ResponseDto(ResponseCode.Success, "Xóa thành công");
        }
    }
}
