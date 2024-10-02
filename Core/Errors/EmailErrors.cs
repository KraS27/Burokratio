using Core.Primitives;

namespace Core.Errors
{
    internal class EmailErrors
    {
        public static Error InvalidEmailLength() =>
            Error.Validation($"Email is required and must be less than {Email.MAX_LENGTH} characters.");

        public static Error InvalidEmailValue() =>
            Error.Validation($"Ivalid email");
    }
}
