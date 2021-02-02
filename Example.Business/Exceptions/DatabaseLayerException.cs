using System;
using System.Runtime.Serialization;

namespace Example.Business.Exceptions
{
    public class DatabaseLayerException : Exception
    {
        public DatabaseLayerException()
        {
        }

        protected DatabaseLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DatabaseLayerException(string? message) : base(message)
        {
        }

        public DatabaseLayerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}