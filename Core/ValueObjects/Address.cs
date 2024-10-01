using Core.Errors;
using Core.Primitives;

namespace Core.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public const int DIVISION_MAX_LENGTH = 128;
        public const int COUNTRY_MAX_LENGTH = 128;
        public const int CITY_MAX_LENGTH = 256;
        public const int STREET_MAX_LENGTH = 256;
        public const int POSTALCODE_MAX_LENGTH = 16;

        private Address(string division, string country, string city, string street, string postalCode)
        {
            Division = division;
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

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

        public static Result<Address> Create(string division, string country, string city, string street, string postalCode)
        {
            if (string.IsNullOrEmpty(division) ||
                division.Length > DIVISION_MAX_LENGTH)
                return AddressErrors.InvalidDivision();

            if (string.IsNullOrEmpty(country) ||
                country.Length > CITY_MAX_LENGTH)
                return AddressErrors.InvalidCountry();

            if (string.IsNullOrEmpty(city) ||
                city.Length > CITY_MAX_LENGTH)
                return AddressErrors.InvalidCity();

            if (string.IsNullOrEmpty(street) ||
                street.Length > STREET_MAX_LENGTH)
                return AddressErrors.InvalidStreet();

            if (string.IsNullOrEmpty(postalCode) ||
                postalCode.Length > POSTALCODE_MAX_LENGTH)
                return AddressErrors.InvalidPostalCode();

            var location = new Address(division, country, city, street, postalCode);

            return location;
        }
    }
}
