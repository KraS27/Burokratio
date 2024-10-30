using Application.DTO.Notar;
using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Core.Errors;
using Core.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace TEST;

public class NotarServiceTests
{
    private static readonly CreateNotarRequest createRequest = new CreateNotarRequest
    ( "John Doe",
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
    private readonly NotarService _notarService;

    public NotarServiceTests()
    {
        _notarRepositoryMock = new Mock<INotarRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _notarService = new NotarService(_notarRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnEmailConflictError_WhenEmailAreSame()
    {
        // Arrange
        var existingNotar = Notar.Create(
            "Existing Notar",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create(createRequest.email).Value!,
            PhoneNumber.Create("+45125112").Value!
        ).Value!;

        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingNotar);  // Return an existing Notar with the same email

        // Act
        var result = await _notarService.AddAsync(createRequest, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NotarErrors.EmailConflict(createRequest.email));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnPhoneNumberConflictError_WhenPhoneNumberAreSame()
    {
        // Arrange
        var existingNotar = Notar.Create(
            "Existing Notar",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create("test@gmail.com").Value!,
            PhoneNumber.Create(createRequest.phoneNumber).Value!
        ).Value!;
        
        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingNotar);  // Return an existing Notar with the same phone
        
        //Act
        var result = await _notarService.AddAsync(createRequest, CancellationToken.None);
        
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(NotarErrors.PhoneNumberConflict(createRequest.phoneNumber));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnSuccessResult_WhenValuesCorrect()
    {
        // Arrange
        var expectedNotar = Notar.Create(
            "Existing Notar",
            Address.Create("Division", "Country", "City", "Street", "12345").Value!,
            Coordinates.Create(45.0, -93.0).Value!,
            Email.Create(createRequest.email).Value!,
            PhoneNumber.Create(createRequest.phoneNumber).Value!
        ).Value!;
        
        _notarRepositoryMock
            .Setup(repo => repo.GetByEmailOrPhoneAsync(It.IsAny<Email>(), It.IsAny<PhoneNumber>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Notar)null);
        
        _notarRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Notar>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _notarService.AddAsync(createRequest, CancellationToken.None);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedNotar.Id);
    }
}