
using System.Net;
using System.Text.Json;
using Azure.Identity;

namespace WebApi.MiddleWares
{
    public class HttpErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpErrorHandlingMiddleware> _logger;

        public HttpErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<HttpErrorHandlingMiddleware> logger)
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext httpContext,
            Exception ex)
        {
            var statusCode = ex switch
            {
                TimeoutException => StatusCodes.Status504GatewayTimeout,
                CredentialUnavailableException => StatusCodes.Status401Unauthorized,
                AuthenticationFailedException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var message = statusCode switch
            {
                StatusCodes.Status504GatewayTimeout => "AI service timeout",
                StatusCodes.Status401Unauthorized => "Unauthorized to access AI service",
                StatusCodes.Status400BadRequest => "Invalid request",
                _ => "An unexpected error occurred222"
            };

            _logger.LogError(
                ex,
                "Request failed with status code {StatusCode}",
                statusCode);

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var error = new
            {
                error = message,
                statusCode,
                traceId = httpContext.TraceIdentifier
            };

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(error));
        }
    }
}


