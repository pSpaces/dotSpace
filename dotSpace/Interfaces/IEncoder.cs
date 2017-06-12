using dotSpace.BaseClasses;
using System;

namespace dotSpace.Interfaces
{
    public interface IEncoder
    {
        T Deserialize<T>(string json, params Type[] types);
        string Serialize(MessageBase message, params Type[] types);
        MessageBase Decode(string msg);
        string Encode(MessageBase message);
    }
}
