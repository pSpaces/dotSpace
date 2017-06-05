using dotSpace.BaseClasses;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net;

namespace dotSpace.Objects.Network.Protocols
{
    public class PullProtocol : ProtocolBase
    {
        public PullProtocol(ServerNode server) : base(server)
        {
        }

        public override void ProcessRequest(ServerSocket socket, BasicRequest request)
        {
        }
        public override T PerformRequest<T>(IPEndPoint endpoint, BasicRequest request)
        {
            return default(T);
        }
    }
}
