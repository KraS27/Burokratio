using Application.DTO.Security;
using Core.Entities;

namespace Application.Interfaces.Auth;

public interface IJwtProvider
{
    public JwtResponse GenerateNotarToken(Notar notar);
}