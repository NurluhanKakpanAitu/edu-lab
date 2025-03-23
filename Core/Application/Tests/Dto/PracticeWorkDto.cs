using Domain.Entities.Base;

namespace Application.Tests.Dto;

public class PracticeWorkDto
{
    public Translation Title { get; set; } = new();
    
    public Translation Description { get; set; } = new();
    
    public string? ImagePath { get; set; }
    
    public Guid ModuleId { get; set; }
}