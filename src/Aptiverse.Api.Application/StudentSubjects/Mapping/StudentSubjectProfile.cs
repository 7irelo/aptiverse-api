using Aptiverse.Api.Application.StudentSubjects.Dtos;
using Aptiverse.Api.Domain.Models.Students;
using AutoMapper;

namespace Aptiverse.Api.Application.StudentSubjects.Mapping
{
    public class StudentSubjectProfile : Profile
    {
        public StudentSubjectProfile()
        {
            CreateMap<StudentSubject, StudentSubjectDto>().ReverseMap();
            CreateMap<StudentSubject, CreateStudentSubjectDto>().ReverseMap();
        }
    }
}
