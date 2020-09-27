using BusTracking.Application.Catalog.StudentService;
using BusTracking.Utilities.Constants;
using BusTracking.ViewModels.Catalog.Stops;
using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
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

        [HttpGet("GetAllPaging")]
        public async Task<PageResultDto<StudentDto>> GetAllPaging([FromQuery]GetStudentPagingRequestDto request)
        {
            var result = await _studentService.GetAllPaging(request);
            return result;
        }

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
