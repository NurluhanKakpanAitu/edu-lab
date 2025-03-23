using Domain.Entities.Base;

namespace Domain.Entities;

public class User : BaseEntity, IAuditable
{
    public required string Nickname { get; set; }
    
    public required string Email { get; set; }
    
    public string? Password { get; set; }
    
    public string? PhotoPath { get; set; }
    
    public Role Role { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpiry { get; set; }
    
    public string? About { get; set; }
    
    public List<UserTestResult> UserTestResults { get; set; } = [];
}