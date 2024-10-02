namespace Core.Primitives
{
    public class Result<TValue>
    {
        public Result(TValue value)
        {           
            Value = value;
            IsSuccess = true;
            Error = Error.None;
        }

        public Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static implicit operator Result<TValue>(TValue value) => new(value);
        public static implicit operator Result<TValue>(Error error) => new(error);

        public Result(TValue value, bool isSuccess, Error? error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
                throw new ArgumentException("Invalid error;", nameof(error));

            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public TValue? Value { get; }

        public bool IsSuccess { get; }

        public Error? Error { get; }

        public bool IsFailure => !IsSuccess;

        public static Result<TValue> Success(TValue value) => new(value);

        public static Result<TValue> Failure(Error error) => new( error);
    }
}
