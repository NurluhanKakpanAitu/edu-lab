using Domain.Entities.Base;

namespace Application.Tests.Vm;

public class PracticeWorkVm
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Translation Title { get; set; } = new();
    
    public Translation Description { get; set; } = new();
    
    public string? ImagePath { get; set; }
}