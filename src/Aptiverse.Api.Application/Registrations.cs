using Aptiverse.Api.Application.Students.Services;
using Aptiverse.Api.Application.StudentSubjects.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aptiverse.Api.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentSubjectsService, StudentSubjectsService>();

            return services;
        }
    }
}