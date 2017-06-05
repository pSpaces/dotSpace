using dotSpace.Enumerations;
using dotSpace.Objects.Network.Messages.Responses;
using System.Net;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public class ClientSocket : SocketBase
    {
        public ClientSocket(TcpClient client) : base(client)
        {

        }

        public ClientSocket(IPEndPoint endpoint) : base(new TcpClient())
        {
            this.client.Connect(endpoint);
        }

        protected override MessageBase Decode<T>(string msg)
        {
            BasicResponse breq = msg.Deserialize<BasicResponse>();
            switch (breq.Action)
            {
                case ActionType.PUT_RESPONSE: breq = msg.Deserialize<PutResponse>(); break;
                case ActionType.GET_RESPONSE: breq = msg.Deserialize<GetResponse>(); break;
                case ActionType.GETP_RESPONSE: breq = msg.Deserialize<GetPResponse>(); break;
                case ActionType.QUERY_RESPONSE: breq = msg.Deserialize<QueryResponse>(); break;
                case ActionType.QUERYP_RESPONSE: breq = msg.Deserialize<QueryPResponse>(); break;
            }

            return breq;
        }
    }
}
