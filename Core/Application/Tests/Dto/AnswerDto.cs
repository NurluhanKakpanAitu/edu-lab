using Domain.Entities.Base;

namespace Application.Tests.Dto;

public class AnswerDto
{
    public Translation Title { get; set; }
    
    public bool IsCorrect { get; set; }
    
    public string? ImagePath { get; set; }
}