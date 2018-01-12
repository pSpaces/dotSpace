using System;
using System.IO;

namespace dotSpace.Interfaces.Network
{
    /// <summary>
    /// Defines the operations required to support encoding and decoding messages from objects to a stream representation.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Composes and returns a new object based on the provided stream.
        /// </summary>
        T Deserialize<T>(Stream stream, params Type[] types);

        /// <summary>
        /// Decomposes the passed object and writes it to the given stream.
        /// </summary>
        int Serialize(Stream stream, IMessage message, params Type[] types);

        /// <summary>
        /// Template method for deserializing and unboxing the interoperable types specified of the stream representation into valid .NET types.
        /// </summary>
        IMessage Decode(Stream stream);

        /// <summary>
        /// Template method for serializing and boxing the passed message into interoperable types specified as a string.
        /// </summary>
        int Encode(Stream stream, IMessage message);
    }
}
