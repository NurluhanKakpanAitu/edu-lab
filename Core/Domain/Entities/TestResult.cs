namespace Domain.Entities;

public class TestResult
{
    public Guid QuestionId { get; set; }
    
    public Guid AnswerId { get; set; }
    
    public bool IsCorrect { get; set; }
}