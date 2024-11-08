namespace Application.DTO.Security;

public record JwtResponse(string AccessToken, DateTime ExpiredIn, string RefreshToken)
{
    
}