using dotSpace.BaseClasses;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System.Net;

namespace dotSpace.Objects.Network.Protocols
{
    public class PushProtocol : ProtocolBase
    {
        private string address;
        private int port;

        public PushProtocol(ServerNode server, string address, int port) : base(server)
        {
            this.address = address;
            this.port = port;
        }

        public override void ProcessRequest(ServerSocket socket, BasicRequest request)
        {
            request = this.ValidateRequest<BasicRequest>(request);
            BasicResponse response = this.operationMap.Execute(request);
            socket.Send(response);
            socket.Close();
        }

        public override T PerformRequest<T>(IPEndPoint endpoint, BasicRequest request)
        {

            TcpListener listener = new TcpListener(this.address, this.port);
            ClientSocket socket = new ClientSocket(endpoint);
            socket.Send(request);
            MessageBase message = socket.Receive<T>();
            socket.Close();
            return this.ValidateResponse<T>(message);
        }


    }
}