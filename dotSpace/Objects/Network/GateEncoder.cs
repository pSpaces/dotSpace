using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Objects.Network
{
    public sealed class GateEncoder : EncoderBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override MessageBase Decode<T>(string msg)
        {
            BasicResponse breq = this.Deserialize<BasicResponse>(msg);
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
            JsonTypeConverter.Unbox(breq);

            return breq;
        }

        public override string Encode(MessageBase message)
        {
            JsonTypeConverter.Box(message);
            return this.Serialize(message, typeof(PatternBinding));
        }

        #endregion
    }
}
