namespace Aptiverse.Api.Application.StudentSubjects.Dtos
{
    public record CreateStudentSubjectDto
    {
        public long StudentId { get; set; }
        public string SubjectId { get; set; }
    }
}
