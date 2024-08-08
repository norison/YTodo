using System.ComponentModel.DataAnnotations;

namespace YTodo.Application.Options;

public class TokenOptions
{
    [Required]
    public string Key { get; set; } = string.Empty;
    
    [Required]
    public string Issuer { get; set; } = string.Empty;
    
    [Required]
    public string Audience { get; set; } = string.Empty;
    
    [Required]
    public TimeSpan ExpirationTimeSpan { get; set; }
}