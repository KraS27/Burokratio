using Core.Errors;
using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public const int MAX_NAME_LENGTH = 256;
        
        public const int MAX_PASSWORD_LENGTH = 16;
        
        public const int MIN_PASSWORD_LENGTH = 8;

        public string Name { get; private set; } = null!;

        public string Password { get; private set; } = null!;
        
        public Address Address { get; private set; } = null!;

        public Coordinates Coordinates { get; private set; } = null!;

        public Email Email { get; private set; } = null!;

        public PhoneNumber PhoneNumber { get; private set; }
        
        public bool IsAuthorized { get; private set; }
        
        private Notar() { }

        private Notar(Guid id,
            string name,
            string password,
            Address address,
            Coordinates coordinates,
            Email email,
            PhoneNumber phoneNumber,
            DateTime createdAt,
            DateTime updatedAt,
            bool isAuthorized
            )
        {
            Name = name;
            Password = password;
            Address = address;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Result<Notar> Create(
            string name,
            string password,
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
                password,
                address,
                coordinates,
                email,
                phoneNumber,
                DateTime.Now,
                DateTime.Now,
                true);

            return notar;
        }
        
        public Result Update(
            string name,
            string password,
            Address address,
            Coordinates coordinates,
            Email email,
            PhoneNumber phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
                return NotarErrors.InvalidName();

            Name = name;
            Password = password;
            Address = address;
            Coordinates = coordinates;
            Email = email;
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.Now;

            return Result.Success();
        }
    }
}
