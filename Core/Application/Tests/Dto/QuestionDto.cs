using Domain.Entities.Base;

namespace Application.Tests;

public class QuestionDto
{
    public Translation Title { get; set; }
    
    public string? ImagePath { get; set; }
    
    public List<AnswerDto> Answers { get; set; }
}