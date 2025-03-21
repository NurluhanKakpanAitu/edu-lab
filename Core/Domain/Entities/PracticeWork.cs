using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class PracticeWork : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    
    public Translation Title { get; set; } = new();
    
    public Translation Description { get; set; } = new();
    
    public string? ImagePath { get; set; }
    
    public Guid ModuleId { get; set; }
    
    [ForeignKey(nameof(ModuleId))]
    public Module? Module { get; set; }
}