using Domain.Entities.Base;

namespace Application.Tests.Vm;

public class TestVm
{
    public required Translation Title { get; set; }
    
    public List<QuestionVm> Questions { get; set; } = [];
    
    public Guid ModuleId { get; set; }
    
    public Guid Id { get; set; }
}


public class QuestionVm
{
    public required Translation Title { get; set; }
    
    public string? ImagePath { get; set; }
    
    public List<AnswerVm> Answers { get; set; } = [];
    
    public Guid Id { get; set; }
}


public class AnswerVm
{
    public required Translation Title { get; set; }
    
    public bool IsCorrect { get; set; }
    
    public string? ImagePath { get; set; }
    
    public Guid Id { get; set; }
}