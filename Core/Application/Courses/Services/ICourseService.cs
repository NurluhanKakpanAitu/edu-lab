using Application.Courses.Dto;
using Application.Courses.Vm;

namespace Application.Courses.Services;

public interface ICourseService
{
    Task<Guid> CreateCourse(CourseDto request, CancellationToken cancellationToken = default);
    
    Task UpdateCourse(Guid courseId, CourseDto request, CancellationToken cancellationToken = default);
    
    Task DeleteCourse(Guid courseId, CancellationToken cancellationToken = default);
    
    Task<List<CourseGetAllVm>> GetAllCourses(CancellationToken cancellationToken = default);
    
    Task<CourseGetByIdVm> GetCourse(Guid courseId, CancellationToken cancellationToken = default);
}