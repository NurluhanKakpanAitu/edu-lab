using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Test : BaseEntity, IAuditable
{
    public required Translation Title { get; set; }
    public Guid ModuleId { get; set; }
    
    [ForeignKey(nameof(ModuleId))]
    public Module? Module { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<Question> Questions { get; set; } = [];
}