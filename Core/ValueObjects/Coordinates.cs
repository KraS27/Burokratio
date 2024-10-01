using Core.Errors;
using Core.Primitives;

namespace Core.ValueObjects
{
    public class Coordinates : ValueObject
    {
        public double Latitude { get; } 

        public double Longitude { get; }

        public const int MIN_LATITUDE_VAUE = -90;
        public const int MAX_LATITUDE_VAUE = 90;
        public const int MIN_LONGITUDE_VAUE = -180;
        public const int MAX_LONGITUDE_VAUE = 180;

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

        public Result<Coordinates> Create(double latitude, double longitude)
        {
            if (latitude < MIN_LATITUDE_VAUE || latitude > MAX_LATITUDE_VAUE)
                return CoordinatesErrors.InvalidLatitude();

            if (longitude < MIN_LONGITUDE_VAUE || longitude > MAX_LONGITUDE_VAUE)
                return CoordinatesErrors.InvalidLongitude();

            var coord = new Coordinates(latitude, longitude);

            return coord;
        }
    }
}
