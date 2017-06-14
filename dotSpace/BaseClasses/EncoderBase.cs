using dotSpace.Interfaces;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.BaseClasses
{
    /// <summary>
    /// Provides basic functionality for serializing and deserializing json objects. This is an abstract class.
    /// </summary>
    public abstract class EncoderBase : IEncoder
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Composes and returns a new object based on the provided json string.
        /// </summary>
        public T Deserialize<T>(string json, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
        /// <summary>
        /// Decomposes the passed object into a json string.
        /// </summary>
        public string Serialize(IMessage message, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(message);
        }
        /// <summary>
        /// Template method for deserializing and unboxing the interoperable types specified in json into valid .NET primitive types.
        /// </summary>
        public abstract IMessage Decode(string msg);
        /// <summary>
        /// Template method for serializing and boxing the passed message into interoperable types specified as a json string.
        /// </summary>
        public abstract string Encode(IMessage message);

        #endregion
    }
}
