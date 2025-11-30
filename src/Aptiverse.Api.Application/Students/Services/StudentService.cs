using Aptiverse.Api.Application.Students.Dtos;
using Aptiverse.Api.Domain.Models.Students;
using Aptiverse.Api.Infrastructure.Data;
using AutoMapper;

namespace Aptiverse.Api.Application.Students.Services
{
    using Aptiverse.Api.Domain.Models.Students;
    using Microsoft.EntityFrameworkCore;

    public class StudentService(ApplicationDbContext dbContext, IMapper mapper) : IStudentService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            ArgumentNullException.ThrowIfNull(createStudentDto);

            Student student = _mapper.Map<Student>(createStudentDto);

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> GetStudentByIdAsync(long id)
        {
            var student = await _dbContext.Students
                .Include(ss => ss.StudentSubjects)
                .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _dbContext.Students.ToListAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto> UpdateStudentAsync(long id, UpdateStudentDto updateStudentDto)
        {
            var existingStudent = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"Student with ID {id} not found");

            _mapper.Map(updateStudentDto, existingStudent);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<StudentDto>(existingStudent);
        }

        public async Task<bool> DeleteStudentAsync(long id)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return false;

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
