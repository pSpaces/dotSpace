using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Responses;
using System.Net;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class ClientSocket : SocketBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ClientSocket(TcpClient client) : base(client)
        {
        } 

        public ClientSocket(IPEndPoint endpoint) : base(new TcpClient())
        {
            this.client.Connect(endpoint);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override MessageBase Decode<T>(string msg)
        {
            BasicResponse breq = msg.Deserialize<BasicResponse>();
            switch (breq.Actiontype)
            {
                case ActionType.GET_RESPONSE: breq = msg.Deserialize<GetResponse>(); break;
                case ActionType.GETP_RESPONSE: breq = msg.Deserialize<GetPResponse>(); break;
                case ActionType.GETALL_RESPONSE: breq = msg.Deserialize<GetAllResponse>(); break;
                case ActionType.QUERY_RESPONSE: breq = msg.Deserialize<QueryResponse>(); break;
                case ActionType.QUERYP_RESPONSE: breq = msg.Deserialize<QueryPResponse>(); break;
                case ActionType.QUERYALL_RESPONSE: breq = msg.Deserialize<QueryAllResponse>(); break;
                case ActionType.PUT_RESPONSE: breq = msg.Deserialize<PutResponse>(); break;
            }

            return breq;
        }

        protected override string Encode(MessageBase message)
        {
            if(message is IReadRequest)
            {
                IReadRequest readRequest = (IReadRequest)message;
                readRequest.Template = JsonTypeConverter.Box(readRequest.Template);
            }
            return message.Serialize(typeof(PatternBinding));
        }

        #endregion
    }
}
