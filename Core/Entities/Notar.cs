using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public const int MAX_NAME_LENGTH = 256;

        public string Name { get;  } = null!;

        public Address Location { get;  } = null!;
        
        public Coordinates Coordinates { get; } = null!;

        public Email Email { get; } = null!;

        public PhoneNumber? PhoneNumber { get; }

        private Notar(string name, Address location, Coordinates coordinates, Email email, PhoneNumber? phoneNumber)
        {
            Name = name;
            Location = location;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Result<Notar> Create(string name, Address location, Coordinates coordinates, Email email, PhoneNumber? phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
