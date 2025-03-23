namespace Domain.Entities.Base;

public class Translation
{
    public string? Ru { get; set; }
    
    public string? Kz { get; set; }
    
    public string? En { get; set; }
    
    
    public string? this[string index] => index switch
    {
        "ru" => Ru,
        "kz" => Kz,
        "en" => En,
        _ => null
    };
 }