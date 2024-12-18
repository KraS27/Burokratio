using Application.Interfaces.Auth;

namespace Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    
    public bool VerifyHash(string hashedPassword, string password) => 
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}