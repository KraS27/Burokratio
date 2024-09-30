namespace Core.Primitives
{
    public class Entity
    {
        public Guid Id { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset UpdatedAt { get; private set; }
    }
}
