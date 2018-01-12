using System;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Binary
{
    [Serializable]
    internal class ClassDictionaryException : Exception
    {
        public ClassDictionaryException(string message) : base(message)
        {
        }

        public ClassDictionaryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClassDictionaryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}