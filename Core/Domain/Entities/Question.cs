using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Question : BaseEntity
{
    public required Translation Title { get; set; }
    
    public string? ImagePath { get; set; }
    
    public int Order { get; set; }
    
    public Guid TestId { get; set; }
    
    [ForeignKey(nameof(TestId))]
    public Test? Test { get; set; }
    
    public List<Answer> Answers { get; set; } = [];
}