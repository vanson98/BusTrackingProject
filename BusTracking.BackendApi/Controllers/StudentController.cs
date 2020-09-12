using BusTracking.Application.Catalog.StudentService;
using BusTracking.ViewModels.Catalog.Students;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTracking.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetStudentPagingRequestDto request)
        {
            var result = await _studentService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
                return BadRequest($"Không tìm thấy đối tượng nào");
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateStudentRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var StudentId = await _studentService.Create(requestDto);
            if (StudentId == 0)
                return BadRequest();
            var Student = await _studentService.GetById(StudentId);
            return CreatedAtAction(nameof(GetById), new { id = StudentId }, Student);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStudentRequestDto request)
        {
            int rowEffected = await _studentService.Update(request);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int rowEffected = await _studentService.Delete(id);
            if (rowEffected == 0)
                return BadRequest();
            return Ok("Xóa thành công");
        }
    }
}
