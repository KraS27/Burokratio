using Application.DTO.Notar;
using Application.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WEB.Extensions;

namespace WEB.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/register")]
    public async Task<IResult> Register([FromBody] RegisterNotarRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterNotarAsync(request, cancellationToken);
        
        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
    
    [HttpPost("/login")]
    public async Task<IResult> Login([FromBody] LoginNotarRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginNotarAsync(request, cancellationToken);
        
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
    }
}