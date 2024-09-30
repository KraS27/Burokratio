using Core.Primitives;

namespace Core.Errors
{
    public static class LocationErrors
    {      
        public static Error InvalidDivision() =>
            Error.Validation($"Division is invalid");

        public static Error InvalidCountry() =>
            Error.Validation($"Country is invalid");

        public static Error InvalidCity() =>
            Error.Validation($"City is invalid");

        public static Error InvalidStreet() =>
            Error.Validation($"Street is invalid");

        public static Error InvalidPostalCode() =>
            Error.Validation($"Postal code is invalid");
    }
}
