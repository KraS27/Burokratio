
using Core.Errors;
using System.Text.RegularExpressions;

namespace Core.Primitives
{
    public class Email : ValueObject
    {
        private const string emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public const int MAX_LENGTH = 256;

        public string Value { get; }

        public Email(string value)
        {
            Value = value;
        }

        public Result<Email> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length > MAX_LENGTH)
                return EmailErrors.InvalidEmailLength();

            if(Regex.IsMatch(input, emailRegex) == false)
                return EmailErrors.InvalidEmailValue();

            var email = input;

            return new Email(email);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
