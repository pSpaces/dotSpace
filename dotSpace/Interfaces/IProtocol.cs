using dotSpace.BaseClasses;

namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines the functionality for communication primitives. 
    /// </summary>
    public interface IProtocol
    {
        /// <summary>
        /// Defines a mechanism to return a decoded message.
        /// </summary>
        MessageBase Receive(IEncoder encoder);
        /// <summary>
        /// Defines a mechanism to encode and send an message.
        /// </summary>
        void Send(MessageBase message, IEncoder encoder);
        /// <summary>
        /// Defines a mechanism to close the communication with the endpoint.
        /// </summary>
        void Close();
    }
}
