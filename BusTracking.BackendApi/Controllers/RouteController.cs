using BusTracking.Application.Catalog.RouteService;
using BusTracking.ViewModels.Catalog.Routes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouteTracking.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetRoutePagingRequestDto request)
        {
            var result = await _routeService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Route = await _routeService.GetById(id);
            if (Route == null)
                return BadRequest($"Không tìm thấy đối tượng nào có id = {id}");
            return Ok(Route);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRouteRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var routeId = await _routeService.Create(requestDto);
            if (routeId == 0)
                return BadRequest();
            var route = await _routeService.GetById(routeId);
            return Ok(route);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRouteRequestDto request)
        {
            int rowEffected = await _routeService.Update(request);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int rowEffected = await _routeService.Delete(id);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Xóa thành công");
        }
    }
}
