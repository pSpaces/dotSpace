using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using System;
using System.IO;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    /// <summary>
    /// Provides access to serialization and deserialization to and from binary.
    /// </summary>
    public class BinaryEncoder : IEncoder
    {
        private BinarySerializer serializer = new BinarySerializer();
        
        /// <summary>
        /// Template method for deserializing and unboxing the interoperable types specified in json into valid .NET primitive types.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public IMessage Decode(Stream stream)
        {
            return Deserialize<IMessage>(stream);
        }

        /// <summary>
        /// Composes and returns a new object based on the provided json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream stream, params Type[] types)
        {
            byte[] lengthbytes = new byte[4];
            stream.Read(lengthbytes, 0, lengthbytes.Length);
            int length = TypeConverter.ToInt32(lengthbytes);
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, bytes.Length);
            return (T) serializer.Deserialize(bytes);
        }

        /// <summary>
        /// Boxes and serializes the passed message into interoperable types specified as a json string.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int Encode(Stream stream, IMessage message)
        {
            return Serialize(stream, message);
        }

        /// <summary>
        /// Decomposes the passed object into a json string.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public int Serialize(Stream stream, IMessage message, params Type[] types)
        {
            byte[] bytes = serializer.Serialize(message);
            byte[] length = TypeConverter.GetBytes(bytes.Length);
            stream.Write(length, 0, length.Length);
            stream.Write(bytes, 0, bytes.Length);
            return length.Length + bytes.Length;
        }
    }
}
