using Core.Entities;

namespace Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateNotarToken(Notar notar);
}