using BusTracking.Application.Catalog.DriverService;
using BusTracking.ViewModels.Catalog.Drivers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracking.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverService _driverService { get; set; }
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetDriverPagingRequestDto request)
        {
            var result = await _driverService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<ActionResult> GetById([FromQuery]int id)
        {
            var driver = await _driverService.GetById(id);
            if (driver == null)
            {
                return BadRequest("Can not find driver");
            }
            return Ok(driver);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateDriverRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var driverId = await _driverService.Create(request);
            if (driverId == 0)
            {
                return BadRequest();
            }
            var driver = await _driverService.GetById(driverId);
            return CreatedAtAction(nameof(GetById), new { id = driverId }, driver);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]UpdateDriverRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var rowEffect = await _driverService.Update(request);
            if (rowEffect == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int rowEffected = await _driverService.Delete(id);
            if (rowEffected == 0)
                return BadRequest();
            return Ok();
        }
    }
}
