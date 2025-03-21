using Domain.Entities.Base;

namespace Domain.Entities;

public class Course : BaseEntity , IAuditable
{
    public required Translation Title { get; set; }
    
    public required Translation Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<Module> Modules { get; set; } = [];
}