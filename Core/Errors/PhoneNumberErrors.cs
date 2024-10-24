using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public class PhoneNumberErrors
    {
        public static Error InvalidValue() =>
            Error.Validation($"Phone number is invalid.");
    }
}
