namespace Application.Users.Dto;

public class UserDto
{
    public required string Nickname { get; set; }
    
    public required string Email { get; set; }
    
    public string? Password { get; set; }
    
    public string? About { get; set; }
    
    public string? PhotoPath { get; set; }
}