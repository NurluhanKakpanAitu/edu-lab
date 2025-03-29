using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PracticeWork : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    
    public required string Title { get; set; } 
    
    public required string Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public Guid ModuleId { get; set; }
    
    [ForeignKey(nameof(ModuleId))]
    public Module? Module { get; set; }
}