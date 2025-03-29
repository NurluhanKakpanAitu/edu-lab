namespace Application.Courses.Dto;

public class CourseDto
{
    public required string Title { get; init; } 
    
    public string? Description { get; set; }
    
    public string? ImagePath { get; set; }
}