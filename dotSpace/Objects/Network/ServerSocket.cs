using dotSpace.Enumerations;
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
                case ActionType.GET_REQUEST: breq = msg.Deserialize<GetRequest>(); break;
                case ActionType.GETP_REQUEST: breq = msg.Deserialize<GetPRequest>(); break;
                case ActionType.GETALL_REQUEST: breq = msg.Deserialize<GetAllRequest>(); break;
                case ActionType.QUERY_REQUEST: breq = msg.Deserialize<QueryRequest>(); break;
                case ActionType.QUERYP_REQUEST: breq = msg.Deserialize<QueryPRequest>(); break;
                case ActionType.QUERYALL_REQUEST: breq = msg.Deserialize<QueryAllRequest>(); break;
                case ActionType.PUT_REQUEST: breq = msg.Deserialize<PutRequest>(); break;
            }

            return breq;
        } 

        #endregion
    }
}
