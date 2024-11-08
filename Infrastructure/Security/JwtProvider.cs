using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO.Security;
using Application.Interfaces.Auth;
using Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public JwtResponse GenerateNotarToken(Notar notar)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)), SecurityAlgorithms.HmacSha256);

        var claims = new Claim[] { new("notarId", notar.Id.ToString()) };

        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpiredHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        var response = new JwtResponse(tokenValue, DateTime.Now.AddHours(_options.ExpiredHours), string.Empty);
        
        return response;
    }
}