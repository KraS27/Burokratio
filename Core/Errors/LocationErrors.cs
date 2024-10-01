using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public static class LocationErrors
    {      
        public static Error InvalidDivision() =>
            Error.Validation($"Division is required and must be less than {Location.DIVISION_MAX_LENGTH} characters.");

        public static Error InvalidCountry() =>
            Error.Validation($"Country is required and must be less than {Location.COUNTRY_MAX_LENGTH} characters.");

        public static Error InvalidCity() =>
            Error.Validation($"City is required and must be less than {Location.CITY_MAX_LENGTH} characters.");

        public static Error InvalidStreet() =>
            Error.Validation($"Street is required and must be less than {Location.STREET_MAX_LENGTH} characters.");

        public static Error InvalidPostalCode() =>
            Error.Validation($"Postal code is required and must be less than {Location.POSTALCODE_MAX_LENGTH} characters.");
    }
}
