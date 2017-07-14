using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network;
using System;

namespace dotSpace.BaseClasses.Network
{
    /// <summary>
    /// Provides basic functionality for supporting multiple connectionschemes, and validating messages. This is an abstract class.
    /// </summary>
    public abstract class ConnectionModeBase : IConnectionMode
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected IProtocol protocol;
        protected IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the ConnectionModeBase class.
        /// </summary>
        public ConnectionModeBase(IProtocol protocol, IEncoder encoder)
        {
            this.protocol = protocol;
            this.encoder = encoder;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Template method for processing requests by the SpaceRepository executing the requested action.
        /// </summary>
        public abstract void ProcessRequest(IOperationMap operationMap);
        /// <summary>
        /// Template method for executing a request by the RemoteSpace.
        /// </summary>
        public abstract T PerformRequest<T>(IMessage request) where T : IMessage;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Validates the passed response. If the message is a response and the response code is OK the message is returned; otherwise an exception is thrown.
        /// </summary>
        protected IMessage ValidateResponse(IMessage message)
        {
            if (message is ResponseBase)
            {
                ResponseBase response = (ResponseBase)message;
                if (response.Code == StatusCode.OK)
                {
                    return (ResponseBase)message;
                }
                throw new Exception(string.Format("{0} - {1}", response.Code, response.Message));
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_RESPONSE, StatusMessage.BAD_RESPONSE));
        }
        /// <summary>
        /// Validates the passed Request. If the message is a request the message is returned; otherwise an exception is thrown.
        /// </summary>
        protected IMessage ValidateRequest(IMessage message)
        {
            if (message is RequestBase)
            {
                return (RequestBase)message;
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_REQUEST, StatusMessage.BAD_REQUEST));
        } 

        #endregion
    }
}
