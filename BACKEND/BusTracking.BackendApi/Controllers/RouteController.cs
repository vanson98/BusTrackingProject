using BusTracking.Application.Catalog.RouteService;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Routes;
using BusTracking.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTracking.BackendApi.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("GetAllRouteUnAssign")]
        public async Task<ResultDto<List<RouteDto>>> GetAllrouteUnAssignAsync()
        {
            var routes = await this._routeService.GetAllRouteUnAssignAsync();
            return new ResultDto<List<RouteDto>>()
            {
                StatusCode = ResponseCode.Success,
                Message = "Thực hiện thành công",
                Result = routes
            };
        }

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<RouteDto>> GetAllPaging([FromQuery]GetRoutePagingRequestDto request)
        {
            var result = await _routeService.GetAllPagingAsync(request);
            return result;
        }

        [HttpGet("Get/{id}")]
        public async Task<ResultDto<RouteDto>> GetById(int id)
        {
            var route = await _routeService.GetByIdAsync(id);
            if (route == null)
            {
                return new ResultDto<RouteDto>()
                {
                    StatusCode = ResponseCode.NotFound,
                    Message = "Không tìm thấy đối tượng",
                    Result = null
                };
            }
            return new ResultDto<RouteDto>(ResponseCode.Success, "Thực thi thành công", route);
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] CreateRouteRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResultDto<RouteDto>(ResponseCode.Validate, "Đầu vào không hợp lệ", null);
            }
            var routeId = await _routeService.CreateAsync(request);
            if (routeId == 0)
            {
                return new ResultDto<RouteDto>(ResponseCode.LogicError, "Tạo không thành công", null);
            }
            return new ResponseDto(ResponseCode.Success, "Tạo mới thành công");
        }

        [HttpPut("Update")]
        public async Task<ResponseDto> Update([FromBody] UpdateRouteRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseDto(ResponseCode.Validate, "Đầu vào không hợp lệ");
            }
            var result = await _routeService.UpdateAsync(request);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Cập nhật không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần cập nhật");
            return new ResponseDto(ResponseCode.Success, "Cập nhật thành công");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            int result = await _routeService.DeleteAsync(id);
            if (result == 0)
                return new ResponseDto(ResponseCode.LogicError, "Xóa không thành công");
            if (result == -1)
                return new ResponseDto(ResponseCode.LogicError, "Không tìm thấy đối tượng cần xóa");
            return new ResponseDto(ResponseCode.Success, "Xóa thành công");
        }
    }
}
