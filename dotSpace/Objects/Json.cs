using dotSpace.Objects.Network;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;

namespace dotSpace.Objects
{
    public static class Json
    {
        public static T Deserialize<T>(this string json)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            return (T)ser.ReadObject(stream);
        }

        public static string Serialize(this MessageBase message)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(message.GetType());
            ser.WriteObject(stream, message);
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
