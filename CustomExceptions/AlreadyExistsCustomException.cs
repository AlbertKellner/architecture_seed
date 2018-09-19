namespace CustomExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class AlreadyExistsCustomException : Exception, ISerializable
    {
        public AlreadyExistsCustomException()
        {
        }

        public AlreadyExistsCustomException(string message) : base(message)
        {
        }
    }
}