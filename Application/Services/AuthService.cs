using Application.DTO.Notar;

namespace Application.Services;

public class AuthService
{
    NotarService _notarService;

    public AuthService(NotarService notarService)
    {
        _notarService = notarService;
    }

    public async Task RegisterNotarAsync(CreateNotarRequest request, CancellationToken cancellationToken)
    {
       await _notarService.AddAsync(request, cancellationToken);
    }

    public async Task LoginNotarAsync(LoginNotarRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}