using dotSpace.BaseClasses;
using dotSpace.Objects.Network.Messages.Requests;

namespace dotSpace.Objects.Network
{
    public class PushProtocol : ProtocolBase
    {
        public PushProtocol(ServerNode server) : base(server)
        {
        }
        public override void Execute(ServerSocket socket, BasicRequest request)
        {

        }
    }
}
