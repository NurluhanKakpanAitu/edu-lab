using Domain.Entities.Base;

namespace Application.Modules;

public class ModuleGetAllVm
{
    public Guid Id { get; set; }
    
    public Translation? Title { get; set; }
    
    public Translation? Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
}