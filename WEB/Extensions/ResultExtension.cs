using Core.Primitives;

namespace WEB.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert a successful result to problem details.");

            return CreateProblemDetails(result.Error!);
        }

        public static IResult ToProblemDetails<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert a successful result to problem details.");

            return CreateProblemDetails(result.Error!);
        }

        private static IResult CreateProblemDetails(Error error)
        {
            return Results.Problem(
                statusCode: GetStatusCode(error.ErrorType),
                title: GetTitle(error.ErrorType),
                type: GetType(error.ErrorType),
                extensions: new Dictionary<string, object?>
                {
                    { nameof(error), new[] { error.Description } }
                }
            );
        }

        static int GetStatusCode(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };

        static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Server Failure"
            };

        static string GetType(ErrorType statusCode) =>
            statusCode switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
    }
}
