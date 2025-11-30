using Aptiverse.Api.Application.Students.Dtos;

namespace Aptiverse.Api.Application.Students.Services
{
    public interface IStudentService
    {
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<StudentDto> GetStudentByIdAsync(long id);
        Task<List<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> UpdateStudentAsync(long id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsync(long id);
    }
}
