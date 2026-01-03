using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var (statusCode, title, detail) = exception switch
        {
            BusinessRuleBrokenException => (
                StatusCodes.Status400BadRequest,
                "Business Rule Violation",
                exception.Message),

            ValidationException valEx => ( // FluentValidation exception
                StatusCodes.Status400BadRequest,
                "Validation Error",
                "One or more validation failures occurred."),

            DbUpdateConcurrencyException => (
                StatusCodes.Status409Conflict,
                "Concurrency Error",
                "The record was modified by another user."),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Server Error",
                "An unexpected error occurred.")
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,
            title = title,
            detail = detail,
            // If it's a validation error, add the specific errors
            errors = (exception as ValidationException)?.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())
        };

        await context.Response.WriteAsJsonAsync(response, ct);
        return true; // Successfully handled
    }
}