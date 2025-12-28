public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // Default to 500
        var result = string.Empty;

        // Custom logic for different exception types
        if (exception is AppValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(new { errors = validationException.Errors });
        }
        else
        {
            result = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
