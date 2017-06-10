using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using System.Net;

namespace dotSpace.Objects.Network.Protocols
{
    public sealed class PullProtocol : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PullProtocol(SpaceRepository repository) : base(repository)
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(Socket socket, BasicRequest request)
        {
        }
        public override T PerformRequest<T>(IPEndPoint endpoint,IEncoder encoder, BasicRequest request)
        {
            return default(T);
        } 

        #endregion
    }
}
