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
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        public static string Serialize(this MessageBase message, params Type[] types)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(message);
        } 

        #endregion
    }
}
