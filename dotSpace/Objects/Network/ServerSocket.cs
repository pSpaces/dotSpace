using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class ServerSocket : SocketBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ServerSocket(TcpClient client) : base(client)
        {

        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override MessageBase Decode<T>(string msg)
        {
            BasicRequest breq = msg.Deserialize<BasicRequest>();
            switch (breq.Action)
            {
                case ActionType.GET_REQUEST: breq = msg.Deserialize<GetRequest>(typeof(Binding)); break;
                case ActionType.GETP_REQUEST: breq = msg.Deserialize<GetPRequest>(typeof(Binding)); break;
                case ActionType.GETALL_REQUEST: breq = msg.Deserialize<GetAllRequest>(typeof(Binding)); break;
                case ActionType.QUERY_REQUEST: breq = msg.Deserialize<QueryRequest>(typeof(Binding)); break;
                case ActionType.QUERYP_REQUEST: breq = msg.Deserialize<QueryPRequest>(typeof(Binding)); break;
                case ActionType.QUERYALL_REQUEST: breq = msg.Deserialize<QueryAllRequest>(typeof(Binding)); break;
                case ActionType.PUT_REQUEST: breq = msg.Deserialize<PutRequest>(); break;
            }
            if (breq is IReadRequest)
            {
                IReadRequest readRequest = (IReadRequest)breq;
                readRequest.Template = JsonTypeConverter.Unbox(readRequest.Template);
            }

            return breq;
        }

        protected override string Encode(MessageBase message)
        {
            return message.Serialize();
        }

        #endregion
    }
}
