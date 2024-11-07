namespace Infrastructure.Security;

public class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    
    public int ExpiredHours { get; set; }
}