using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;

namespace dotSpace.BaseClasses.Network
{
    /// <summary>
    /// Provides a basic template for communication primitives. This is an abstract class.
    /// </summary>
    public abstract class ProtocolBase : IProtocol
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Template method returning a decoded message.
        /// </summary>
        public abstract IMessage Receive(IEncoder encoder);
        /// <summary>
        /// Template method for sending an encoded message.
        /// </summary>
        public abstract void Send(IMessage message, IEncoder encoder);
        /// <summary>
        /// Closes the communication with the endpoint.
        /// </summary>
        public abstract void Close(); 

        #endregion
    }
}
