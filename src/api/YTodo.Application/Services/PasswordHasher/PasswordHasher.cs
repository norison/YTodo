using System.Diagnostics.CodeAnalysis;

namespace YTodo.Application.Services.PasswordHasher;

[ExcludeFromCodeCoverage]
public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}