using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Objects.Network.Messages.Requests;

namespace dotSpace.Objects.Network
{
    public sealed class RepositoryEncoder : EncoderBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override MessageBase Decode<T>(string msg)
        {
            BasicRequest breq = Deserialize<BasicRequest>(msg);
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

            JsonTypeConverter.Unbox(breq);
            return breq;
        }

        public override string Encode(MessageBase message)
        {
            JsonTypeConverter.Box(message);
            return this.Serialize(message);
        }

        #endregion
    }
}
