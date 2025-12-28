public class AppValidationException(IEnumerable<string> errors)
    : Exception("One or more validation failures have occurred.")
{
    public IEnumerable<string> Errors { get; } = errors;
}
