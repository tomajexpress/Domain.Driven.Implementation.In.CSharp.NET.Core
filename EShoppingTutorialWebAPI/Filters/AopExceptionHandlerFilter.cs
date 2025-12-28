namespace EShoppingTutorialWebAPI.Filters;

public class AopExceptionHandlerFilter : IExceptionFilter, IOrderedFilter
{
    public int Order { get; } = int.MaxValue - 10;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is null) 
            return;

        // 1. Handle Domain/Business Rule violations
        if (context.Exception is BusinessRuleBrokenException exception)
        {
            var result = new
            {
                error = "Business Rule Violation",
                message = exception.Message
            };
            context.Result = new BadRequestObjectResult(result);
            context.ExceptionHandled = true;
            return;
        }

        // 2. Handle Database Concurrency issues
        if (context.Exception is DbUpdateConcurrencyException)
        {
            context.Result = new ConflictObjectResult(new
            {
                error = "Concurrency Error",
                message = "The record was modified by another user. Please refresh and try again."
            });
            context.ExceptionHandled = true;
            return;
        }

        // 3. Fallback for unexpected errors
        context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        context.ExceptionHandled = true;
    }
}
