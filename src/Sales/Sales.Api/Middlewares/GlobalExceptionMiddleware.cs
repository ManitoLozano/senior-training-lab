using System.Text.Json;
using FluentValidation;

namespace Sales.Api.Middlewares;

public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ValidationException exception)
        {
            logger.LogWarning(exception, "Validation error occurred");

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                title = "Validation Error",
                status = StatusCodes.Status400BadRequest,
                detail = "One or more errors occurred.",
                errors = exception.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList(),
                traceId = httpContext.TraceIdentifier
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Bad Request error occurred");

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                title = "Bad Request Error",
                status = StatusCodes.Status400BadRequest,
                detail = exception.Message,
                traceId = httpContext.TraceIdentifier
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (InvalidOperationException exception)
        {
            logger.LogWarning(exception, "Business error occurred.");

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                title = "Business Error",
                status = StatusCodes.Status400BadRequest,
                detail = exception.Message,
                traceId = httpContext.TraceIdentifier
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Resource not found");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                title = "Resource not found",
                status = StatusCodes.Status500InternalServerError,
                detail = exception.Message,
                traceId = httpContext.TraceIdentifier
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Unexpected error occurred");
            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                title = "Unexpected error",
                status = StatusCodes.Status500InternalServerError,
                detail = exception.Message,
                traceId = httpContext.TraceIdentifier
            };
            
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}