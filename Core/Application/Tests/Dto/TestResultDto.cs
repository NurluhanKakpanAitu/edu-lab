using Domain.Entities;

namespace Application.Tests.Dto;

public class TestResultDto
{
    public Guid TestId { get; set; }
    
    public List<TestResult> Results { get; set; } = [];
}