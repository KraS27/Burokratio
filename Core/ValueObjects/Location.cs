using Core.Primitives;

namespace Core.ValueObjects
{
    public sealed class Location : ValueObject
    {
        public const int DIVISION_MAX_LENGTH = 128;
        public const int COUNTRY_MAX_LENGTH = 128;
        public const int CITY_MAX_LENGTH = 256;
        public const int STREET_MAX_LENGTH = 256;
        public const int POSTALCODE_MAX_LENGTH = 16;

        public string Division { get; } = null!;

        public string Country { get; } = null!;

        public string City { get; } = null!;

        public string Street { get; } = null!;

        public string PostalCode { get; } = null!;

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Division;
            yield return Country; 
            yield return City; 
            yield return Street; 
            yield return PostalCode;
        }

        public static Result Create(string division, string country, string city, string street, string postalCode)
        {
            throw new NotImplementedException();
        }
    }
}
