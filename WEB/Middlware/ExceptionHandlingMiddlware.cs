using Microsoft.AspNetCore.Mvc;

namespace WEB.Middlware
{
    public class ExceptionHandlingMiddlware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddlware> _logger;

        public ExceptionHandlingMiddlware(RequestDelegate next, ILogger<ExceptionHandlingMiddlware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Exception occured: {ex.Message}");

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server error",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
