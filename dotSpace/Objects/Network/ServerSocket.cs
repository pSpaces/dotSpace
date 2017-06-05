using dotSpace.Enumerations;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public class ServerSocket : SocketBase
    {
        public ServerSocket(TcpClient client) : base(client)
        {

        }
        protected override MessageBase Decode<T>(string msg)
        {
            BasicRequest breq = msg.Deserialize<BasicRequest>();
            switch (breq.Action)
            {
                case ActionType.PUT_REQUEST: breq = msg.Deserialize<PutRequest>(); break;
                case ActionType.GET_REQUEST: breq = msg.Deserialize<GetRequest>(); break;
                case ActionType.GETP_REQUEST: breq = msg.Deserialize<GetPRequest>(); break;
                case ActionType.QUERY_REQUEST: breq = msg.Deserialize<QueryRequest>(); break;
                case ActionType.QUERYP_REQUEST: breq = msg.Deserialize<QueryPRequest>(); break;
            }

            return breq;
        }
    }
}
