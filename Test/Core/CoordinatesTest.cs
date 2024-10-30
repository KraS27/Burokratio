using Core.Errors;
using Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace TEST.Core;

public class CoordinatesTest
{
    [Fact]
    public void Create_ShouldReturnCoordinates_WhenValuesCorrect()
    {
        //Arrange
        double longitude = 56.6;
        double latitude = 56.6;
        
        //Act
        var result = Coordinates.Create(latitude, longitude);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Longitude.Should().Be(longitude);
        result.Value.Latitude.Should().Be(latitude);
    }

    [Fact]
    public void Crete_ShouldReturnInvalidLatitudeError_WhenLatitudeIsOutOfRange()
    {
        //Arrange
        double longitude = 56.6;
        double latitude = 180;
        
        //Act
        var result = Coordinates.Create(latitude, longitude);
        
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CoordinatesErrors.InvalidLatitude());
    }
    
    [Fact]
    public void Crete_ShouldReturnInvalidLongitudeError_WhenLongitudeIsOutOfRange()
    {
        //Arrange
        double longitude = -360;
        double latitude = 89;
        
        //Act
        var result = Coordinates.Create(latitude, longitude);
        
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CoordinatesErrors.InvalidLongitude());
    }
}