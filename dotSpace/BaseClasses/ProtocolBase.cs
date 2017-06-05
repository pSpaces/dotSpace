using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.BaseClasses
{
    public abstract class ProtocolBase
    {
        protected ServerNode server;

        public ProtocolBase(ServerNode server)
        {
            this.server = server;
        }
        public abstract void Execute(ServerSocket socket, BasicRequest request);
    }
}
