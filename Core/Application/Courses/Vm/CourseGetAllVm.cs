using Domain.Entities.Base;

namespace Application.Courses.Vm;

public class CourseGetAllVm
{
    public Guid Id { get; set; }
    
    public Translation Title { get; set; } = null!;
    
    public Translation Description { get; set; } = null!;
    
    public string? ImagePath { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}