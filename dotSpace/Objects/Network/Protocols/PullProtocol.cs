using dotSpace.BaseClasses;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net;

namespace dotSpace.Objects.Network.Protocols
{
    public sealed class PullProtocol : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PullProtocol(Node server) : base(server)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(NodeSocket socket, BasicRequest request)
        {
        }
        public override T PerformRequest<T>(IPEndPoint endpoint, BasicRequest request)
        {
            return default(T);
        } 

        #endregion
    }
}
