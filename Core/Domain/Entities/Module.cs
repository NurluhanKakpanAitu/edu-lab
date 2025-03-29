using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Module : BaseEntity, IAuditable
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public string? VideoPath { get; set; }
    
    public int Order { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid CourseId { get; set; }
    
    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }
    
    public string? TaskPath { get; set; }
    
    public List<PracticeWork> PracticeWorks { get; init; } = [];
    
    public string? PresentationPath { get; set; }
}