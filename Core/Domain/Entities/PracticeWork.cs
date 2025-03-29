using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class PracticeWork : BaseEntity, IAuditable
{
    public string? Title { get; set; } 
    
    public string? Description { get; set; }
    
    public string? ImagePath { get; set; }
    
    public Guid ModuleId { get; set; }
    
    [ForeignKey(nameof(ModuleId))]
    public Module? Module { get; set; }

    public DateTime CreatedAt { get; set; }
}