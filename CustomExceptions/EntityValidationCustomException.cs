namespace CustomExceptions
{
    using System;
    using System.Runtime.Serialization;

    public class EntityValidationCustomException : Exception, ISerializable
    {
        public EntityValidationCustomException()
        {
        }

        public EntityValidationCustomException(string message) : base(message)
        {
        }
    }
}