using Aptiverse.Api.Application.Students.Dtos;
using Aptiverse.Api.Application.Students.Services;
using Aptiverse.Api.Application.StudentSubjects.Dtos;
using Aptiverse.Api.Application.StudentSubjects.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aptiverse.Api.Controllers
{
    [Route("api/student-subjects")]
    [ApiController]
    public class StudentSubjectsController(IStudentSubjectsService studentSubjectsService, ILogger<StudentSubjectsController> logger) : ControllerBase
    {
        private readonly IStudentSubjectsService _studentSubjectsService = studentSubjectsService;
        private readonly ILogger<StudentSubjectsController> _logger = logger;

        [HttpPost]
        public async Task<ActionResult<StudentSubjectDto>> CreateStudentSubjects([FromBody] CreateStudentSubjectDto createStudentSubjectDto)
        {
            try
            {
                var createdStudentSubject = await _studentSubjectsService.CreateStudentSubjectAsync(createStudentSubjectDto);
                return CreatedAtAction(nameof(GetStudentSubject), new { id = createdStudentSubject.Id }, createdStudentSubject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student");
                return BadRequest(new { message = "Error creating student", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSubjectDto>>> GetAllStudentSubjects([FromQuery] long studentId)
        {
            try
            {
                var students = await _studentSubjectsService.GetManyStudentSubjectsAsync(studentId);
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all students");
                return StatusCode(500, new { message = "Error retrieving students", error = ex.Message });
            }
        }

        [HttpGet("{studentId}/{subjectId}")]
        public async Task<ActionResult<StudentDto>> GetStudentSubject(long studentId, string subjectId)
        {
            try
            {
                var studentSubject = await _studentSubjectsService.GetStudentSubjectByIdAsync(studentId, subjectId);

                if (studentSubject == null)
                    return NotFound(new { message = $"Student subject with ID {studentId} not found" });

                return Ok(studentSubject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student subject with ID {StudentSubjectId}", studentId);
                return StatusCode(500, new { message = "Error retrieving student subject", error = ex.Message });
            }
        }
    }
}
