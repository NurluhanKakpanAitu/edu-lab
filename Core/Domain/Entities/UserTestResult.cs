using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserTestResult : BaseEntity
{
    public Guid UserId { get; set; }
    
    public Guid TestId { get; set; }
    
    public List<TestResult> TestResults { get; set; } = [];
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    
    [ForeignKey(nameof(TestId))]
    public Test? Test { get; set; }

    public int MaxScore => TestResults.Select(x => x.QuestionId).Count();
    
    public int Score => TestResults.Count(x => x.IsCorrect);
}