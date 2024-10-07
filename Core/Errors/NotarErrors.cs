using Core.Entities;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public class NotarErrors
    {
        public static Error InvalidName() =>
            Error.Validation($"Name is required and must be less than {Notar.MAX_NAME_LENGTH} characters.");

        public static Error NotFound(Guid id) =>
            Error.NotFound($"Notar with id: {id} not found.");

        public static Error EmailConflict(string email) =>
            Error.Conflict($"Notar with email: {email} already exist.");

        public static Error PhoneNumberConflict(string phone) =>
            Error.Conflict($"Notar with phone number: {phone} already exist.");
    }
}
