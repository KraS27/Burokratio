using Core.Primitives;

namespace Core.Errors
{
    public class PhoneNumberErrors
    {
        public static Error InvalidNumber() =>
            Error.Validation($"Phone number is invalid.");
    }
}
