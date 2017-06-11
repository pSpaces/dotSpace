using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System.Net;
using System.Net.Sockets;

namespace dotSpace.Objects.Network.Protocols
{
    public sealed class Push : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Push(ISocket socket, IEncoder encoder) : base(socket, encoder)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(OperationMap operationMap)
        {
            BasicRequest request = (BasicRequest)this.socket.Receive(this.encoder);
            request = this.ValidateRequest(request);
            BasicResponse response = operationMap.Execute(request);
            this.socket.Send(response, this.encoder);
            this.socket.Close();
        }

        public override T PerformRequest<T>(BasicRequest request)
        {
            this.socket.Send(request, this.encoder);
            MessageBase message = socket.Receive(this.encoder);
            this.socket.Close();
            return (T)this.ValidateResponse(message);
        }

        #endregion
    }
}