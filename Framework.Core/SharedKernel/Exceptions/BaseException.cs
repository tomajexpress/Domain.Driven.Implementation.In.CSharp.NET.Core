using System;

namespace SharedKernel.Exceptions
{
    [Serializable]
    public class BaseException : ApplicationException
    {
        public BaseException()
        {

        }

        public BaseException(string message) : base(message)
        {

        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
