namespace CustomExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class NotFoundCustomException : Exception, ISerializable
    {
        public NotFoundCustomException()
        {
        }

        public NotFoundCustomException(string message) : base(message)
        {
        }
    }
}