using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Objects.Network.ConnectionModes
{
    /// <summary>
    /// Implements the mechanisms to support the PULL connection scheme.
    /// This connection mode is incomplete, and is thus not supported.
    /// </summary>
    public sealed class Pull : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the Pull class. 
        /// </summary>
        public Pull(IProtocol protocol, IEncoder encoder) : base(protocol, encoder)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override void ProcessRequest(IOperationMap operationMap)
        {
        }
        /// <summary>
        /// Not implemented.
        /// </summary>
        public override T PerformRequest<T>(IMessage request)
        {
            IMessage message = new BasicResponse(request.Actiontype, request.Source, request.Session, request.Target, StatusCode.NOT_IMPLEMENTED, StatusMessage.NOT_IMPLEMENTED);
            this.protocol.Close();
            return (T)this.ValidateResponse(message);
        }

        #endregion
    }
}