using BusTracking.Application.Catalog.StopService;
using BusTracking.ViewModels.Catalog.Stops;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StopTracking.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StopController : ControllerBase
    {
        private readonly IStopService _stopService;
        public StopController(IStopService stopService)
        {
            _stopService = stopService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetStopPagingReqestDto request)
        {
            var result = await _stopService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stop = await _stopService.GetById(id);
            if (stop == null)
                return BadRequest($"Không tìm thấy đối tượng nào");
            return Ok(stop);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateStopRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var stopId = await _stopService.Create(requestDto);
            if (stopId == 0)
                return BadRequest();
            var stop = await _stopService.GetById(stopId);
            return CreatedAtAction(nameof(GetById), new { id = stopId }, stop);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateStopRequestDto request)
        {
            int rowEffected = await _stopService.Update(request);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int rowEffected = await _stopService.Delete(id);
            if (rowEffected == 0)
                return BadRequest("Xóa không thành công");
            return Ok("Xóa thành công");
        }
    }
}
