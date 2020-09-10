using BusTracking.ViewModels.Catalog.Students;
using BusTracking.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Application.Catalog.StudentService
{
    public interface IStudentService
    {
        Task<PageResultDto<StudentDto>> GetAllPaging(GetStudentPagingRequestDto request);
        Task<StudentDto> GetById(int busId);
        Task<int> Create(CreateStudentRequestDto requestDto);
        Task<int> Update(UpdateStudentRequestDto requestDto);
        Task<int> Delete(int StudentId);
    }
}
