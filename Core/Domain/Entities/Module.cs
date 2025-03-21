using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Module : BaseEntity, IAuditable
{
    public required Translation Title { get; set; }
    
    public required Translation Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid CourseId { get; set; }
    
    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }
    
    public List<Test> Tests { get; set; } = [];
}