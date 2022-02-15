using System.Runtime.Serialization;

namespace WorkSchedule.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public int ErrorCode { get; }

        public ApplicationException()
        {
        }

        protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApplicationException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApplicationException(int errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
