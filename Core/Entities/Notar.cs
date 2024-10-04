using Core.Errors;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public const int MAX_NAME_LENGTH = 256;

        public string Name { get;  } = null!;

        public Address Address { get;  } = null!;
        
        public Coordinates Coordinates { get; } = null!;

        public Email Email { get; } = null!;

        public PhoneNumber? PhoneNumber { get; }

        private Notar(string name, Address address, Coordinates coordinates, Email email, PhoneNumber? phoneNumber)
        {
            Name = name;
            Address = address;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Result<Notar> Create(string name, Address address, Coordinates coordinates, Email email, PhoneNumber? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                return NotarErrors.InvalidName();

            var notar = new Notar(name, address, coordinates, email, phoneNumber);

            return notar;
        }
    }
}
