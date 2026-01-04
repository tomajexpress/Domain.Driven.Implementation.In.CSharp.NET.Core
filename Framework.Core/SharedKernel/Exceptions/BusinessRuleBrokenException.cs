namespace SharedKernel.Exceptions;

/// <summary>
/// Exception thrown when a specific domain business rule is violated.
/// </summary>
public class BusinessRuleBrokenException(string message, Exception? innerException = null)
    : BaseException(message, innerException);