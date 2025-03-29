using Domain.Entities.Base;

namespace Domain.Entities;

public class Course : BaseEntity , IAuditable
{
    public string? Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<Module> Modules { get; set; } = [];
}