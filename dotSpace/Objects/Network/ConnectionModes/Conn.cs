using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;

namespace dotSpace.Objects.Network.ConnectionModes
{
    /// <summary>
    /// Implements the mechanisms to support the CONN connection scheme.
    /// </summary>
    public sealed class Conn : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the Conn class.
        /// </summary>
        public Conn(IProtocol protocol, IEncoder encoder) : base(protocol, encoder)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Waits for an incoming message, then executes the corresponding operation and transmits a response.
        /// </summary>
        public override void ProcessRequest(IOperationMap operationMap)
        {
            RequestBase request = (RequestBase)this.protocol.Receive(this.encoder);
            request = (RequestBase)this.ValidateRequest(request);
            ResponseBase response = (ResponseBase)operationMap.Execute(request);
            this.protocol.Send(response, this.encoder);
        }
        /// <summary>
        /// Sends a request and waits for a response. Finally, it closes the connection and returns the received message.
        /// This is a blocking operation.
        /// </summary>
        public override T PerformRequest<T>(IMessage request)
        {
            this.protocol.Send(request, this.encoder);
            MessageBase message = (MessageBase)protocol.Receive(this.encoder);
            this.protocol.Close();
            return (T)this.ValidateResponse(message);
        } 

        #endregion

    }
}
