using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Messages.Responses;
using System.IO;

namespace dotSpace.Objects.Network.Json
{
    /// <summary>
    /// Provides basic functionality for serializing and deserializing requests as json string. 
    /// Furthermore, the underlying values are boxed and unboxed thereby supporting language independent types.
    /// </summary>
    public sealed class JsonRequestEncoder : JsonEncoderBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Unboxes and deserializes the passed json string containing interoperable types to a message containine .NET primitive types.
        /// </summary>
        public override IMessage Decode(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string msg = reader.ReadLine();

            MemoryStream memory = new MemoryStream();
            StreamWriter writer = new StreamWriter(memory);
            writer.Write(msg);
            writer.Flush();
            memory.Position = 0;

            ResponseBase breq = this.Deserialize<BasicResponse>(memory);
            memory.Position = 0;
            switch (breq.Actiontype)
            {
                case ActionType.GET_RESPONSE: breq = this.Deserialize<GetResponse>(memory); ((GetResponse)breq).Result = TypeConverter.Unbox(((GetResponse)breq).Result); break;
                case ActionType.GETP_RESPONSE: breq = this.Deserialize<GetPResponse>(memory); ((GetPResponse)breq).Result = TypeConverter.Unbox(((GetPResponse)breq).Result); break;
                case ActionType.GETALL_RESPONSE: breq = this.Deserialize<GetAllResponse>(memory); ((GetAllResponse)breq).Result = TypeConverter.Unbox(((GetAllResponse)breq).Result); break;
                case ActionType.QUERY_RESPONSE: breq = this.Deserialize<QueryResponse>(memory); ((QueryResponse)breq).Result = TypeConverter.Unbox(((QueryResponse)breq).Result); break;
                case ActionType.QUERYP_RESPONSE: breq = this.Deserialize<QueryPResponse>(memory); ((QueryPResponse)breq).Result = TypeConverter.Unbox(((QueryPResponse)breq).Result); break;
                case ActionType.QUERYALL_RESPONSE: breq = this.Deserialize<QueryAllResponse>(memory); ((QueryAllResponse)breq).Result = TypeConverter.Unbox(((QueryAllResponse)breq).Result); break;
                case ActionType.PUT_RESPONSE: breq = this.Deserialize<PutResponse>(memory); break;
            }
            
            return breq;
        }

        #endregion
    }
}
