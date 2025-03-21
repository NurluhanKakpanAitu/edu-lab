using Domain.Entities.Base;

namespace Application.Courses.Dto;

public class CourseDto
{
    public Translation Title { get; set; } = null!;
    
    public Translation Description { get; set; } = null!;
    
    public string? ImagePath { get; set; }
}