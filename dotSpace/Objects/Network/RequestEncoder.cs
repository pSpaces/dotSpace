using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Messages.Responses;
using System.IO;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// Provides basic functionality for serializing and deserializing requests as json string. 
    /// Furthermore, the underlying values are boxed and unboxed thereby supporting language independent types.
    /// </summary>
    public sealed class RequestEncoder : EncoderBase
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
                case ActionType.GET_RESPONSE: breq = this.Deserialize<GetResponse>(memory); break;
                case ActionType.GETP_RESPONSE: breq = this.Deserialize<GetPResponse>(memory); break;
                case ActionType.GETALL_RESPONSE: breq = this.Deserialize<GetAllResponse>(memory); break;
                case ActionType.QUERY_RESPONSE: breq = this.Deserialize<QueryResponse>(memory); break;
                case ActionType.QUERYP_RESPONSE: breq = this.Deserialize<QueryPResponse>(memory); break;
                case ActionType.QUERYALL_RESPONSE: breq = this.Deserialize<QueryAllResponse>(memory); break;
                case ActionType.PUT_RESPONSE: breq = this.Deserialize<PutResponse>(memory); break;
            }

            breq.Unbox();
            return breq;
        }

        #endregion
    }
}
