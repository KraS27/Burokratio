using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public class PhoneNumberErrors
    {
        public static Error InvalidNumberLength() =>
            Error.Validation($"Phone number is required and must be less than {PhoneNumber.MAX_LENGTH}.");

        public static Error InvalidNumberValue() =>
            Error.Validation($"Phone number is invalid.");
    }
}
