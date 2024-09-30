using Core.Primitives;

namespace Core.ValueObjects
{
    public sealed class Location : ValueObject
    {
        public string Division { get; } = null!;

        public string Country { get; } = null!;

        public string City { get; } = null!;

        public string Street { get; } = null!;

        public override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
