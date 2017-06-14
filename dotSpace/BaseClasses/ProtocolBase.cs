using dotSpace.Interfaces;

namespace dotSpace.BaseClasses
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
        public abstract MessageBase Receive(IEncoder encoder);
        /// <summary>
        /// Template method for sending an encoded message.
        /// </summary>
        public abstract void Send(MessageBase message, IEncoder encoder);
        /// <summary>
        /// Closes the communication with the endpoint.
        /// </summary>
        public abstract void Close(); 

        #endregion
    }
}
