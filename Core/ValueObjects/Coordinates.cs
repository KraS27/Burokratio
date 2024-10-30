using Core.Errors;
using Core.Primitives;

namespace Core.ValueObjects
{
    public class Coordinates : ValueObject
    {
        public double Latitude { get; } 

        public double Longitude { get; }

        public const int MIN_LATITUDE_VALUE = -90;
        public const int MAX_LATITUDE_VALUE = 90;
        public const int MIN_LONGITUDE_VALUE = -180;
        public const int MAX_LONGITUDE_VALUE = 180;

        private Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Latitude;
            yield return Longitude;
        }

        public static Result<Coordinates> Create(double latitude, double longitude)
        {
            if (latitude < MIN_LATITUDE_VALUE || latitude > MAX_LATITUDE_VALUE)
                return CoordinatesErrors.InvalidLatitude();

            if (longitude < MIN_LONGITUDE_VALUE || longitude > MAX_LONGITUDE_VALUE)
                return CoordinatesErrors.InvalidLongitude();

            var coord = new Coordinates(latitude, longitude);

            return coord;
        }
    }
}
