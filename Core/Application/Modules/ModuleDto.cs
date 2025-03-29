namespace Application.Modules;

public class ModuleDto
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
    
    public Guid CourseId { get; set; }
    
    public string? TaskPath { get; set; }
    
    public string? PresentationPath { get; set; }
}