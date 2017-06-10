using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System.Net;
using System.Net.Sockets;

namespace dotSpace.Objects.Network.Protocols
{

    public sealed class ConnProtocol : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ConnProtocol(RepositoryBase repository) : base(repository)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(Socket socket, BasicRequest request)
        {
            request = this.ValidateRequest<BasicRequest>(request);
            BasicResponse response = this.operationMap.Execute(request);
            socket.Send(response);
            socket.Close();
        }

        public override T PerformRequest<T>(IPEndPoint endpoint, IEncoder encoder, BasicRequest request)
        {
            TcpClient client = new TcpClient();
            client.Connect(endpoint);
            Socket socket = new Socket(client, encoder);
            socket.Send(request);
            MessageBase message = socket.Receive<T>();
            socket.Close();
            return this.ValidateResponse<T>(message);
        } 

        #endregion

    }
}
