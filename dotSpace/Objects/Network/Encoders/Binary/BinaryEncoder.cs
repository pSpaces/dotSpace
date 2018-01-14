using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Encoders.Binary.Utilities;
using System;
using System.IO;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    public sealed class BinaryEncoder : IEncoder
    {
        private BinarySerializer serializer = new BinarySerializer();

        public int Serialize(Stream stream, IMessage message, params Type[] types)
        {
            BinarySerializer serializer = new BinarySerializer();
            var msg = serializer.Serialize(message);
            var bytelength = TypeConverter.GetBytes(msg.Length);
            stream.Write(bytelength, 0, bytelength.Length);
            stream.Write(msg, 0, msg.Length);
            stream.Flush();
            return msg.Length;
        }

        public T Deserialize<T>(Stream stream, params Type[] types)
        {
            BinarySerializer serializer = new BinarySerializer();
            byte[] lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, lengthBytes.Length);
            int length = TypeConverter.ToInt32(lengthBytes);
            byte[] bytes = new byte[length];
            stream.Read(bytes, 0, bytes.Length);
            return (T)serializer.Deserialize(typeof(T), bytes);
        }

        public int Encode(Stream stream, IMessage message)
        {
            int n = this.Serialize(stream, message);
            return n;
        }

        public IMessage Decode(Stream stream)
        {
            MessageBase breq = this.Deserialize<MessageBase>(stream);
            return breq;
        }
    }
}
