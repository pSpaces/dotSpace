using dotSpace.BaseClasses;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;

namespace dotSpace.Objects.Network
{

    public class ConnProtocol : ProtocolBase
    {
        private TSOperationMap operationMap;

        public ConnProtocol(ServerNode server) : base(server)
        {
            this.operationMap = new TSOperationMap(server);
        }

        public override void Execute(ServerSocket socket, BasicRequest request)
        {
            BasicResponse response = this.operationMap.ExecuteRequest(request);            
            socket.Send(response);
        }
    }
}
