namespace Domain.Entities.Base;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
}