using Application.DTO.Notar;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Errors;
using Core.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace TEST.Application;

public class AuthServiceTests
{
    private static readonly CreateNotarRequest createRequest = new CreateNotarRequest
    ( "John Doe",
        "unreal@52strong616&%",
        "Division",
        "Country",
        "City",
        "street",
        "12345",
        45.0,
        -93.0,
        "existing@example.com",
        "+380976222512"
    );
    
    private readonly Mock<INotarRepository> _notarRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _notarRepositoryMock = new Mock<INotarRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _authService = new AuthService(_passwordHasherMock.Object, _notarRepositoryMock.Object, _unitOfWorkMock.Object, _jwtProviderMock.Object);
    }

    [Fact]
    public async Task RegisterNotarAsync_ShouldReturnEmailConflictError_WhenEmailAreSame()
    {
        // Arrange
        var existingNotar = Notar.Create(
            "Existing Notar",
            "unreal@52strong616&%",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create(createRequest.Email).Value!,
            PhoneNumber.Create("+45125112").Value!
        ).Value!;

        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingNotar);  // Return an existing Notar with the same email

        // Act
        var result = await _authService.RegisterNotarAsync(createRequest, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NotarErrors.EmailConflict(createRequest.Email));
    }

    [Fact]
    public async Task RegisterNotarAsync_ShouldReturnPhoneNumberConflictError_WhenPhoneNumberAreSame()
    {
        // Arrange
        var existingNotar = Notar.Create(
            "Existing Notar",
            "unreal@52strong616&%",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create("test@gmail.com").Value!,
            PhoneNumber.Create(createRequest.PhoneNumber).Value!
        ).Value!;
        
        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingNotar);  // Return an existing Notar with the same phone
        
        //Act
        var result = await _authService.RegisterNotarAsync(createRequest, CancellationToken.None);
        
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NotarErrors.PhoneNumberConflict(createRequest.PhoneNumber));
    }

    [Fact]
    public async Task RegisterNotarAsync_ShouldReturnSuccessResult_WhenValuesCorrect()
    {
        // Arrange
        var expectedNotar = Notar.Create(
            "Existing Notar",
            "unreal@52strong616&%",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create(createRequest.Email).Value!,
            PhoneNumber.Create(createRequest.PhoneNumber).Value!
        ).Value!;
        
        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Notar)null);
        
        _notarRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Notar>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _authService.RegisterNotarAsync(createRequest, CancellationToken.None);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}