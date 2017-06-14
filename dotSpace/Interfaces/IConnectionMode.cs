using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Interfaces
{
    /// <summary>
    /// This interface specifies the operations required to support any given connection mode.
    /// </summary>
    public interface IConnectionMode
    {
        /// <summary>
        /// Method for processing requests by the SpaceRepository executing the requested action.
        /// </summary>
        void ProcessRequest(IOperationMap operationMap);
        /// <summary>
        /// Method for executing a request by the RemoteSpace.
        /// </summary>
        T PerformRequest<T>(IMessage request) where T : IMessage;
    }
}
