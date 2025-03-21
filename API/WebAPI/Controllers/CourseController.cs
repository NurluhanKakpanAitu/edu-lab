using Application.Courses.Dto;
using Application.Courses.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseDto request, CancellationToken cancellationToken = default)
    {
        var courseId = await courseService.CreateCourse(request, cancellationToken);
        return ResponseOk(courseId);
    }
    
    [HttpPut("{courseId:guid}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] Guid courseId, [FromBody] CourseDto request, CancellationToken cancellationToken = default)
    {
        await courseService.UpdateCourse(courseId, request, cancellationToken);
        return ResponseOk();
    }
    
    
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId, CancellationToken cancellationToken = default)
    {
        await courseService.DeleteCourse(courseId, cancellationToken);
        return ResponseOk();
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCourses(CancellationToken cancellationToken = default)
    {
        var courses = await courseService.GetAllCourses(cancellationToken);
        return ResponseOk(courses);
    }
    
    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetCourse([FromRoute] Guid courseId, CancellationToken cancellationToken = default)
    {
        var course = await courseService.GetCourse(courseId, cancellationToken);
        return ResponseOk(course);
    }
}