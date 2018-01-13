using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Json;
using System;
using System.IO;
using System.Web.Script.Serialization;

namespace dotSpace.BaseClasses.Network.Json
{
    /// <summary>
    /// Provides basic functionality for serializing and deserializing json objects. This is an abstract class.
    /// </summary>
    public abstract class JsonEncoderBase : IEncoder
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Composes and returns a new object based on the provided json string.
        /// </summary>
        public T Deserialize<T>(Stream stream, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StreamReader reader = new StreamReader(stream);
            string msg = reader.ReadLine();
            return serializer.Deserialize<T>(msg);
        }

        /// <summary>
        /// Decomposes the passed object into a json string.
        /// </summary>
        public int Serialize(Stream stream, IMessage message, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StreamWriter writer = new StreamWriter(stream);
            string msg = serializer.Serialize(message);
            writer.WriteLine(msg);
            writer.Flush();
            return msg.Length;
        }

        /// <summary>
        /// Boxes and serializes the passed message into interoperable types specified as a json string.
        /// </summary>
        public int Encode(Stream stream, IMessage message)
        {
            if (message is TemplateRequestBase)
                ((TemplateRequestBase)message).Template = TypeConverter.Box(((TemplateRequestBase)message).Template);
            else if (message is ReturnResponseBase)
                ((ReturnResponseBase)message).Result = TypeConverter.Box(((ReturnResponseBase)message).Result);
            return this.Serialize(stream, message);
        }

        /// <summary>
        /// Template method for deserializing and unboxing the interoperable types specified in json into valid .NET primitive types.
        /// </summary>
        public abstract IMessage Decode(Stream stream);

        #endregion
    }
}
