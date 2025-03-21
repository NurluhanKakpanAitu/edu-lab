using Domain.Entities.Base;

namespace Application.Tests;

public class TestDto
{
    public Translation Title { get; set; }
    
    public List<QuestionDto> Questions { get; set; }
    
    public Guid ModuleId { get; set; }
}