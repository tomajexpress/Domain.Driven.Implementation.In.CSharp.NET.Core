namespace SharedKernel.Exceptions;

/// <summary>
/// Base exception for all domain-specific exceptions.
/// Inheriting from Exception is the modern standard over ApplicationException.
/// </summary>
public class BaseException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);