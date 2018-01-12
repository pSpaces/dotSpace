using dotSpace.Interfaces.Network;
using org.dotspace.io.binary;
using System;
using System.IO;

namespace dotSpace.Objects.Network.Encoders.Binary
{
    class BinaryEncoder : IEncoder
    {
        public IMessage Decode(Stream msg)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(Stream stream, params Type[] types)
        {
            throw new NotImplementedException();
        }

        public int Encode(Stream stream, IMessage message)
        {
            throw new NotImplementedException();
        }

        public int Serialize(Stream stream, IMessage message, params Type[] types)
        {
            throw new NotImplementedException();
        }
    }
}
