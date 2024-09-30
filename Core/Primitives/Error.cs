namespace Core.Primitives
{
    public sealed record class Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        public string Code { get; } 

        public string? Description { get; }

        public ErrorType ErrorType { get; }

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        public static Error Validation(string code, string description) =>
           new(code, description, ErrorType.Validation);

        public static Error Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);

        public static Error Failure (string code, string description) =>
            new(code, description, ErrorType.Failure);
    }
}
