using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Json;
using dotSpace.Objects.Network.Messages.Requests;
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
        public override IMessage Decode(string msg)
        {
            RequestBase breq = Deserialize<BasicRequest>(msg);
            switch (breq.Actiontype)
            {
                case ActionType.GET_REQUEST: breq = this.Deserialize<GetRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.GETP_REQUEST: breq = this.Deserialize<GetPRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.GETALL_REQUEST: breq = this.Deserialize<GetAllRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.QUERY_REQUEST: breq = this.Deserialize<QueryRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.QUERYP_REQUEST: breq = this.Deserialize<QueryPRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.QUERYALL_REQUEST: breq = this.Deserialize<QueryAllRequest>(msg, typeof(PatternBinding)); break;
                case ActionType.PUT_REQUEST: breq = this.Deserialize<PutRequest>(msg); break;
            }

            breq.Unbox();
            return breq;
        }

        #endregion
    }
}
