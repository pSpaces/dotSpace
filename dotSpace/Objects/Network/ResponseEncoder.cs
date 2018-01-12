using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Json;
using dotSpace.Objects.Network.Messages.Requests;
using System.IO;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// Provides basic functionality for serializing and deserializing responses as json string. 
    /// Furthermore, the underlying values are boxed and unboxed thereby supporting language independent types.
    /// </summary>
    public sealed class ResponseEncoder : EncoderBase
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

            RequestBase breq = Deserialize<BasicRequest>(memory);
            memory.Position = 0;
            switch (breq.Actiontype)
            {
                case ActionType.GET_REQUEST: breq = this.Deserialize<GetRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.GETP_REQUEST: breq = this.Deserialize<GetPRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.GETALL_REQUEST: breq = this.Deserialize<GetAllRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.QUERY_REQUEST: breq = this.Deserialize<QueryRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.QUERYP_REQUEST: breq = this.Deserialize<QueryPRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.QUERYALL_REQUEST: breq = this.Deserialize<QueryAllRequest>(memory, typeof(PatternBinding)); break;
                case ActionType.PUT_REQUEST: breq = this.Deserialize<PutRequest>(memory); break;
            }

            breq.Unbox();
            return breq;
        }

        #endregion
    }
}
