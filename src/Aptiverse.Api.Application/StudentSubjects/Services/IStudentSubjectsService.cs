using Aptiverse.Api.Application.StudentSubjects.Dtos;

namespace Aptiverse.Api.Application.StudentSubjects.Services
{
    public interface IStudentSubjectsService
    {
        Task<StudentSubjectDto> CreateStudentSubjectAsync(CreateStudentSubjectDto createStudentSubjectDto);
        Task<StudentSubjectDto> GetStudentSubjectByIdAsync(long studentId, string subjectId);
        Task<IEnumerable<StudentSubjectDto>> GetManyStudentSubjectsAsync(long studentId);
    }
}
