using Domain.Entities.Base;

namespace Application.Modules;

public class ModuleDto
{
    public required Translation Title { get; set; }
    
    public required Translation Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
    
    public Guid CourseId { get; set; }
}