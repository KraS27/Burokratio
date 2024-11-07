using Core.Entities;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Errors
{
    public class NotarErrors
    {
        public static Error InvalidName() =>
            Error.Validation($"Name is required and must be less than {Notar.MAX_NAME_LENGTH} characters.");

        public static Error IdNotFound(Guid id) =>
            Error.NotFound($"Notar with id: {id} not found.");

        public static Error EmailNotFound(string email) =>
            Error.NotFound($"Notar with email: {email} not found.");
        
        public static Error EmailConflict(string email) =>
            Error.Conflict($"Notar with email: {email} already exist.");

        public static Error PhoneNumberConflict(string phone) =>
            Error.Conflict($"Notar with phone number: {phone} already exist.");
        
        public static Error InvalidPasswordLength() =>
            Error.Validation($"Password length must be at least {Notar.MIN_PASSWORD_LENGTH} and maximum {Notar.MAX_PASSWORD_LENGTH} characters.");
        
        public static Error InvalidPassword() =>
            Error.Validation($"Invalid password.");
    }
}
