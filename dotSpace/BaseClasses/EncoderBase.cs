using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.BaseClasses
{
    public abstract class EncoderBase : IEncoder
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public T Deserialize<T>(string json, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
        public string Serialize(MessageBase message, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(message);
        }
        public abstract MessageBase Decode(string msg); 
        public abstract string Encode(MessageBase message);

        #endregion
    }
}
