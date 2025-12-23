using System;

namespace SharedKernel.Exceptions;

[Serializable]
public class BusinessRuleBrokenException : BaseException
{
    public BusinessRuleBrokenException(string message) : base(message)
    {
    }

    public BusinessRuleBrokenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
