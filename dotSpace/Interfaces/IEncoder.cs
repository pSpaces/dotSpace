using dotSpace.Objects.Network;
using System;

namespace dotSpace.Interfaces
{
    public interface IEncoder
    {
        T Deserialize<T>(string json, params Type[] types);
        string Serialize(MessageBase message, params Type[] types);
        MessageBase Decode<T>(string msg) where T : MessageBase;
        string Encode(MessageBase message);
    }
}
