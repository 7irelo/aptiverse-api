﻿using Aptiverse.Application.Teachers.Dtos;

namespace Aptiverse.Application.Teachers.Services
{
    public class TeacherService : ITeacherService
    {
        public Task<TeacherDto> CreateTeacherAsync(TeacherDto teacherDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeacherAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeacherDto>> GetManyTeachersAsync(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<TeacherDto> GetOneTeacherAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TeacherDto> UpdateTeachersAsync(long id, TeacherDto teacherDto)
        {
            throw new NotImplementedException();
        }
    }
}
