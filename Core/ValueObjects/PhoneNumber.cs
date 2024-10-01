using Core.Errors;
using Core.Primitives;
using System.Text.RegularExpressions;

namespace Core.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private const string phoneRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        public string Number { get; }

        private PhoneNumber(string number)
        {
            Number = number;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
        }

        public static Result<PhoneNumber> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, phoneRegex) == false)
                return PhoneNumberErrors.InvalidNumber();

            var number = new PhoneNumber(input);

            return number;
        }

    }
}
