using dotSpace.Objects.Network;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.Objects
{
    public static class Json
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public static T Deserialize<T>(this string json, params Type[] types)
        {
            //MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), types);
            //return (T)ser.ReadObject(stream);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        public static string Serialize(this MessageBase message, params Type[] types)
        {
            //MemoryStream stream = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(message.GetType(), types);
            //ser.WriteObject(stream, message);
            //return Encoding.UTF8.GetString(stream.ToArray());

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(message);
        } 

        #endregion
    }
}
