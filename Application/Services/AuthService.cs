using Application.DTO.Notar;
using Application.DTO.Security;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Core.Entities;
using Core.Errors;
using Core.Primitives;
using Core.ValueObjects;

namespace Application.Services;

public class AuthService
{
    private readonly INotarRepository _notarRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IPasswordHasher passwordHasher, INotarRepository notarRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _notarRepository = notarRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result> RegisterNotarAsync(RegisterNotarRequest request, CancellationToken cancellationToken)
    { 
        var emailAndPhoneResult = await CheckEmailAndPhoneAsync(request.Email, request.PhoneNumber,cancellationToken);

        if (emailAndPhoneResult.IsFailure)
            return emailAndPhoneResult.Error!;
           
        (Email email, PhoneNumber phone) = emailAndPhoneResult.Value;
           
        var addressResult = Address.Create(request.Division, request.Country, request.City, request.Street, request.PostalCode);
        var coordinatesResult = Coordinates.Create(request.Latitude, request.Longitude);

        if(addressResult.IsFailure || coordinatesResult.IsFailure)
            return addressResult.Error ?? coordinatesResult.Error!;
            
        var password = request.Password;
        
        if (password.Length < Notar.MIN_PASSWORD_LENGTH || password.Length > Notar.MAX_PASSWORD_LENGTH)
            return NotarErrors.InvalidPasswordLength();
        
        var hashedPassword = _passwordHasher.GenerateHash(password);
        
        Result<Notar> notarResult = Notar.Create(
            request.Name, 
            hashedPassword,
            addressResult.Value!, 
            coordinatesResult.Value!,
            email,
            phone
        ).Value!;

        if (notarResult.IsFailure)
            return notarResult.Error!;

        await _notarRepository.AddAsync(notarResult.Value!, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    private async Task<Result<(Email email, PhoneNumber phoneNumber)>> CheckEmailAndPhoneAsync(string email, string phone, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(email);
        var phoneResult = PhoneNumber.Create(phone);
            
        if (emailResult.IsFailure || phoneResult.IsFailure)
            return emailResult.Error ?? phoneResult.Error!;
            
        var notar = await _notarRepository.GetByEmailOrPhoneAsync(emailResult.Value!, phoneResult.Value!, cancellationToken);
            
        if (notar != null && notar.Email.Equals(emailResult.Value))
            return NotarErrors.EmailConflict(notar.Email.Value);
            
        if(notar != null && notar.PhoneNumber.Equals(phoneResult.Value))
            return NotarErrors.PhoneNumberConflict(notar.PhoneNumber.Value);
            
        return Result<(Email email, PhoneNumber phoneNumber)>.Success((emailResult.Value!, phoneResult.Value!));
    }
    
    public async Task<Result<JwtResponse>> LoginNotarAsync(LoginNotarRequest request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        
        if(emailResult.IsFailure)
            return emailResult.Error!;
        
        var notar = await _notarRepository.GetByEmailAsync(emailResult.Value!, cancellationToken);
        
        if(notar == null)
            return NotarErrors.EmailNotFound(request.Email);
        
        var passwordResult = _passwordHasher.VerifyHash(notar.Password, request.Password);

        if (passwordResult == false)
            NotarErrors.InvalidPassword();

        var tokenResponse = _jwtProvider.GenerateNotarToken(notar);

        return tokenResponse;
    }
}