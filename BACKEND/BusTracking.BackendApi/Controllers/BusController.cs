using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTracking.Application.Catalog.BusService;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Buses;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.BackendApi.Controllers
{
    [Authorize(Roles = "monitor")]
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<BusDto>> GetAllPaging([FromQuery]GetBusPagingRequestDto request)
        {
            var result = await _busService.GetAllPagingAsync(request);
            if(result.StatusCode == ResponseCode.Success)
            {
                result.Message = "Thành công";
            }
            return result;
        }

        [HttpGet("Get")]
        public async Task<ResultDto<BusDto>> GetById([FromQuery]int id)
        {
            var bus = await _busService.GetByIdAsync(id);
            if (bus == null)
            {
                return new ResultDto<BusDto>()
                {
                    StatusCode = ResponseCode.NotFound,
                    Message = "Không tìm thấy đối tượng",
                    Result = null
                };
            }
            return new ResultDto<BusDto>(ResponseCode.Success, "Thực thi thành công", bus);
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody]CreateBusRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResultDto<BusDto>(ResponseCode.Validate, "Đầu vào không hợp lệ", null);
            }
            var busId = await _busService.CreateAsync(request);
            if (busId == 0)
            {
                return new ResultDto<BusDto>(ResponseCode.LogicError, "Tạo mới không thành công", null);
            }
            return new ResponseDto(ResponseCode.Success, "Tạo mới thành công");
        }

        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody]UpdateBusRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _busService.UpdateAsync(request);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần cập nhật");
            return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
        }

        [HttpDelete("Delete")]
        public async Task<ResponseDto> Delete([FromQuery]int id)
        {
            int result = await _busService.DeleteAsync(id);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Xóa không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần xóa");
            return new ResponseDto(ResponseCode.Success, "Xóa thành công");
        }
    }
}
