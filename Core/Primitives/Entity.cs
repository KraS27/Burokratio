namespace Core.Primitives
{
    public class Entity
    {
        public Guid Id { get; }

        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset UpdatedAt { get; }
    }
}
