namespace Core.Primitives
{
    public sealed record class Error
    {
        public static readonly Error None = new(string.Empty, ErrorType.Failure);

        public string? Description { get; }

        public ErrorType ErrorType { get; }

        private Error(string description, ErrorType errorType)
        {         
            Description = description;
            ErrorType = errorType;
        }

        public static Error NotFound(string description) =>
            new(description, ErrorType.NotFound);

        public static Error Validation(string description) =>
           new(description, ErrorType.Validation);

        public static Error Conflict(string description) =>
            new(description, ErrorType.Conflict);

        public static Error Failure (string description) =>
            new(description, ErrorType.Failure);
    }
}
