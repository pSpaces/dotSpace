using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Messages.Responses;

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
        public override IMessage Decode(string msg)
        {
            ResponseBase breq = this.Deserialize<BasicResponse>(msg);
            switch (breq.Actiontype)
            {
                case ActionType.GET_RESPONSE: breq = this.Deserialize<GetResponse>(msg); break;
                case ActionType.GETP_RESPONSE: breq = this.Deserialize<GetPResponse>(msg); break;
                case ActionType.GETALL_RESPONSE: breq = this.Deserialize<GetAllResponse>(msg); break;
                case ActionType.QUERY_RESPONSE: breq = this.Deserialize<QueryResponse>(msg); break;
                case ActionType.QUERYP_RESPONSE: breq = this.Deserialize<QueryPResponse>(msg); break;
                case ActionType.QUERYALL_RESPONSE: breq = this.Deserialize<QueryAllResponse>(msg); break;
                case ActionType.PUT_RESPONSE: breq = this.Deserialize<PutResponse>(msg); break;
            }

            breq.Unbox();
            
            return breq;
        }

        #endregion
    }
}
