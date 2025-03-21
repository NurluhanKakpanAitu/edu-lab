using Domain;

namespace Application.Users.Vm;

public class UserVm
{
    public Guid Id { get; init; }
    
    public string Email { get; init; } = null!;
    
    public string Nickname { get; init; } = null!;
    
    public Role Role { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public string? PhotoPath { get; init; }
    
    public string? About { get; init; }
}