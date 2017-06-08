using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class NodeSocket : SocketBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public NodeSocket(TcpClient client) : base(client)
        {

        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override MessageBase Decode<T>(string msg)
        {
            BasicRequest breq = msg.Deserialize<BasicRequest>();
            switch (breq.Actiontype)
            {
                case ActionType.GET_REQUEST: breq = msg.Deserialize<GetRequest>(typeof(PatternBinding)); break;
                case ActionType.GETP_REQUEST: breq = msg.Deserialize<GetPRequest>(typeof(PatternBinding)); break;
                case ActionType.GETALL_REQUEST: breq = msg.Deserialize<GetAllRequest>(typeof(PatternBinding)); break;
                case ActionType.QUERY_REQUEST: breq = msg.Deserialize<QueryRequest>(typeof(PatternBinding)); break;
                case ActionType.QUERYP_REQUEST: breq = msg.Deserialize<QueryPRequest>(typeof(PatternBinding)); break;
                case ActionType.QUERYALL_REQUEST: breq = msg.Deserialize<QueryAllRequest>(typeof(PatternBinding)); break;
                case ActionType.PUT_REQUEST: breq = msg.Deserialize<PutRequest>(); break;
            }

            JsonTypeConverter.Unbox(breq);
            return breq;
        }

        protected override string Encode(MessageBase message)
        {
            JsonTypeConverter.Box(message);
            return message.Serialize();
        }

        #endregion
    }
}
