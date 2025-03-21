using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Answer : BaseEntity
{
    public required Translation Title { get; set; }
    
    public bool IsCorrect { get; set; }
    
    public Guid QuestionId { get; set; }
    
    [ForeignKey(nameof(QuestionId))]
    public Question? Question { get; set; }
    
    public string? ImagePath { get; set; }
}