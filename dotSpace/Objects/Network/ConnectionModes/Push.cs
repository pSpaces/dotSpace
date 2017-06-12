using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Objects.Network.ConnectionModes
{
    public sealed class Push : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Push(IProtocol socket, IEncoder encoder) : base(socket, encoder)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(OperationMap operationMap)
        {
            BasicRequest request = (BasicRequest)this.protocol.Receive(this.encoder);
            request = this.ValidateRequest(request);
            BasicResponse response = operationMap.Execute(request);
            this.protocol.Send(response, this.encoder);
            this.protocol.Close();
        }

        public override T PerformRequest<T>(BasicRequest request)
        {
            this.protocol.Send(request, this.encoder);
            MessageBase message = protocol.Receive(this.encoder);
            this.protocol.Close();
            return (T)this.ValidateResponse(message);
        }

        #endregion
    }
}