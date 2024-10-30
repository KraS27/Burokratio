using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public static class CoordinatesErrors
    {
        public static Error InvalidLatitude() =>
            Error.Validation($"Latitude value must be between {Coordinates.MIN_LATITUDE_VALUE} and {Coordinates.MAX_LONGITUDE_VALUE}.");

        public static Error InvalidLongitude() =>
            Error.Validation($"Longitude value must be between {Coordinates.MIN_LONGITUDE_VALUE} and {Coordinates.MAX_LONGITUDE_VALUE}.");
    }
}
