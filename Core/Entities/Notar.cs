using Core.Errors;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public const int MAX_NAME_LENGTH = 256;

        public string Name { get; private set; } = null!;

        public Address Address { get; private set; } = null!;

        public Coordinates Coordinates { get; private set; } = null!;

        public Email Email { get; private set; } = null!;

        public PhoneNumber PhoneNumber { get; private set; }

        private Notar() { }

        private Notar(Guid id,
            string name,
            Address address,
            Coordinates coordinates,
            Email email,
            PhoneNumber phoneNumber,
            DateTime createdAt,
            DateTime updatedAt)
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
            PhoneNumber phoneNumber)
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
                DateTime.Now,
                DateTime.Now);

            return notar;
        }
        
        public Result Update(string name,
            Address address,
            Coordinates coordinates,
            Email email,
            PhoneNumber phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                return NotarErrors.InvalidName();

            Name = name;
            Address = address;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.Now;

            return Result.Success();
        }
    }
}
