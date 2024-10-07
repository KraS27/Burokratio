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

        private Notar(Guid id, 
            string name, 
            Address address, 
            Coordinates coordinates,
            Email email,
            PhoneNumber? phoneNumber, 
            DateTimeOffset createdAt, 
            DateTimeOffset updatedAt)
        {
            Name = name;
            Address = address;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Result<Notar> Create(string name, 
            Address address, 
            Coordinates coordinates, 
            Email email, 
            PhoneNumber? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                return NotarErrors.InvalidName();

            var notar = new Notar(
                Guid.NewGuid(),
                name, 
                address, 
                coordinates,
                email, 
                phoneNumber,
                DateTimeOffset.Now,
                DateTimeOffset.Now);

            return notar;
        }
    }
}
