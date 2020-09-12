﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTracking.Application.Catalog.BusService;
using BusTracking.ViewModels.Catalog.Buses;
using Microsoft.AspNetCore.Mvc;

namespace BusTracking.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetBusPagingRequestDto request)
        {
            var result = await _busService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bus = await _busService.GetById(id);
            if (bus == null)
                return BadRequest($"Không tìm thấy đối tượng nào có id = {id}");
            return Ok(bus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBusRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var busId = await _busService.Create(requestDto);
            if (busId == 0)
                return BadRequest();
            var bus = await _busService.GetById(busId);
            return CreatedAtAction(nameof(GetById), new { id = busId }, bus);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateBusRequestDto request)
        {
            int rowEffected = await _busService.Update(request);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int rowEffected = await _busService.Delete(id);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Xóa thành công");
        }
    }
}
