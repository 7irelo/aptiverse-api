using Aptiverse.Api.Application.Students.Dtos;
using Aptiverse.Api.Application.Students.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aptiverse.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController(IStudentService studentService, ILogger<StudentsController> logger) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;
        private readonly ILogger<StudentsController> _logger = logger;

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            try
            {
                var createdStudent = await _studentService.CreateStudentAsync(createStudentDto);
                return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student");
                return BadRequest(new { message = "Error creating student", error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(long id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);

                if (student == null)
                    return NotFound(new { message = $"Student with ID {id} not found" });

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving student with ID {StudentId}", id);
                return StatusCode(500, new { message = "Error retrieving student", error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Superuser")]
        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all students");
                return StatusCode(500, new { message = "Error retrieving students", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(long id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudentAsync(id, updateStudentDto);
                return Ok(updatedStudent);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Student with ID {StudentId} not found for update", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student with ID {StudentId}", id);
                return BadRequest(new { message = "Error updating student", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            try
            {
                var result = await _studentService.DeleteStudentAsync(id);

                if (!result)
                    return NotFound(new { message = $"Student with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student with ID {StudentId}", id);
                return StatusCode(500, new { message = "Error deleting student", error = ex.Message });
            }
        }
    }
}