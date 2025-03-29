namespace Application.Courses.Vm;

public class CourseVm
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<ModuleVm> Modules { get; set; } = [];
}


public class ModuleVm
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? TaskPath { get; set; }
    
    public string? PresentationPath { get; set; }
    
    public Guid CourseId { get; set; }
}