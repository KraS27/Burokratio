﻿namespace Core.Primitives
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }
    }
}
