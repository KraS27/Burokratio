using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public static class AddressErrors
    {      
        public static Error InvalidDivision() =>
            Error.Validation($"Division is required and must be less than {Address.DIVISION_MAX_LENGTH} characters.");

        public static Error InvalidCountry() =>
            Error.Validation($"Country is required and must be less than {Address.COUNTRY_MAX_LENGTH} characters.");

        public static Error InvalidCity() =>
            Error.Validation($"City is required and must be less than {Address.CITY_MAX_LENGTH} characters.");

        public static Error InvalidStreet() =>
            Error.Validation($"Street is required and must be less than {Address.STREET_MAX_LENGTH} characters.");

        public static Error InvalidPostalCode() =>
            Error.Validation($"Postal code is required and must be less than {Address.POSTALCODE_MAX_LENGTH} characters.");
    }
}
