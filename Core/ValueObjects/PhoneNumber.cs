using Core.Errors;
using Core.Primitives;
using System.Text.RegularExpressions;

namespace Core.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string PHONE_REGEX = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
        public const int MAX_LENGTH = 32;

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<PhoneNumber> Create(string input)
        {
            if(Regex.IsMatch(input, PHONE_REGEX) == false)
                return PhoneNumberErrors.InvalidValue();

            var number = new PhoneNumber(input);

            return number;
        }

    }
}
