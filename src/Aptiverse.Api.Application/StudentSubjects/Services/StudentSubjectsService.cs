using Aptiverse.Api.Application.Students.Dtos;
using Aptiverse.Api.Application.StudentSubjects.Dtos;
using Aptiverse.Api.Domain.Models.Students;
using Aptiverse.Api.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aptiverse.Api.Application.StudentSubjects.Services
{
    public class StudentSubjectsService(ApplicationDbContext dbContext, IMapper mapper) : IStudentSubjectsService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<StudentSubjectDto> CreateStudentSubjectAsync(CreateStudentSubjectDto createStudentSubjectDto)
        {
            ArgumentNullException.ThrowIfNull(createStudentSubjectDto);

            StudentSubject studentSubject = _mapper.Map<StudentSubject>(createStudentSubjectDto);

            await _dbContext.StudentSubjects.AddAsync(studentSubject);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<StudentSubjectDto>(studentSubject);
        }

        public async Task<StudentSubjectDto> GetStudentSubjectByIdAsync(long studentId, string subjectId)
        {
            var studentSubject = await _dbContext.StudentSubjects
                .Include(ss => ss.Student)
                .Include(ss => ss.Subject)
                .Include(ss => ss.Analytics)
                .Include(ss => ss.WeeklyStudyHours)
                .Include(ss => ss.TopicPerformances)
                .Include(ss => ss.ImprovementTips)
                .Include(ss => ss.KnowledgeGaps)
                .FirstOrDefaultAsync(ss => ss.StudentId == studentId && ss.SubjectId == subjectId);

            return _mapper.Map<StudentSubjectDto>(studentSubject);
        }

        public async Task<IEnumerable<StudentSubjectDto>> GetManyStudentSubjectsAsync(long studentId)
        {
            var studentSubjects = await _dbContext.StudentSubjects.ToListAsync();
            return _mapper.Map<IEnumerable<StudentSubjectDto>>(studentSubjects);
        }
    }
}
