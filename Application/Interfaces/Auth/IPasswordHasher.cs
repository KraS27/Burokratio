namespace Application.Interfaces.Auth;

public interface IPasswordHasher
{
    public string GenerateHash(string password);

    public bool VerifyHash(string hashedPassword, string password);
}