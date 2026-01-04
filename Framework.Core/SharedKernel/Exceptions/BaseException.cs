namespace SharedKernel.Exceptions;

/// <summary>
/// Base exception for all domain-specific exceptions.
/// </summary>
public class BaseException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);