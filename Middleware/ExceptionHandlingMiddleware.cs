using System.Text.Json;

namespace Zeiss.ProductApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";

                var response = new { message = "An unexpected error occurred. Please try again later." };
                var jsonResponse = JsonSerializer.Serialize(response);

                await httpContext.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
