using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public int? StatusCode { get; }

        protected BaseException() { }
        protected BaseException(string? message) : base(message) { }
        protected BaseException(string? message, Exception? innerException) : base(message, innerException) { }
        protected BaseException(int statusCode, string? message) : base(message) { StatusCode = statusCode; }
        protected BaseException(int statusCode, string? message, Exception? innerException) : base(message, innerException) { StatusCode = statusCode; }
        protected BaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
