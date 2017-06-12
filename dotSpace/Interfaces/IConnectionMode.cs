using dotSpace.Objects;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Interfaces
{
    public interface IConnectionMode
    {
        void ProcessRequest(OperationMap operationMap);
        T PerformRequest<T>(BasicRequest request) where T : BasicResponse;
    }
}
