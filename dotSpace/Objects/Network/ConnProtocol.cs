using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.Objects.Network
{
    public abstract class Protocol
    {
        protected ServerNode server;

        public Protocol(ServerNode server)
        {
            this.server = server;
        }
        public abstract void Execute(TcpClient client, BasicRequest request);
    }

    public class ConnProtocol : Protocol
    {
        public ConnProtocol(ServerNode server) : base(server)
        {
        }
        public override void Execute(TcpClient client, BasicRequest request)
        {
            ITupleSpace tuplespace = server[request.Target];
            ProcessRequest process = new ProcessRequest(tuplespace);
            process.Execute(request);
            //client.
        }
    }

    public class PushProtocol : Protocol
    {
        public PushProtocol(ServerNode server) : base(server)
        {
        }
        public override void Execute(TcpClient client, BasicRequest request)
        {

        }
    }

    public class PullProtocol : Protocol
    {
        public PullProtocol(ServerNode server) : base(server)
        {
        }

        public override void Execute(TcpClient client, BasicRequest request)
        {

        }
    }
}
