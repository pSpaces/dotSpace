using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Protocols;
using System.Collections.Generic;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public class ServerNode : NodeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields
        private TcpListener listener;
        private Dictionary<string, ITupleSpace> spaces;
        private Dictionary<ConnectionMode, ProtocolBase> protocols;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ServerNode(int port, string address = "", bool listenOnStart = true) : base(address, port)
        {
            this.listener = new TcpListener(this.address, this.port);
            this.spaces = new Dictionary<string, ITupleSpace>();
            this.protocols = new Dictionary<ConnectionMode, ProtocolBase>();
            this.protocols.Add(ConnectionMode.CONN, new ConnProtocol(this));
            //this.protocols.Add(ConnectionMode.PUSH, new PushProtocol(this));
            //this.protocols.Add(ConnectionMode.PULL, new PushProtocol(this));
            if (listenOnStart)
            {
                this.StartListen();
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public ITupleSpace this[string indexer]
        {
            get
            {
                if (this.spaces.ContainsKey(indexer))
                {
                    return this.spaces[indexer];
                }
                return null;
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override ITuple Get(string target, IPattern pattern)
        {
            return this[target]?.Get(pattern);
        }
        public override ITuple GetP(string target, IPattern pattern)
        {
            return this[target]?.GetP(pattern);
        }
        public override ITuple Query(string target, IPattern pattern)
        {
            return this[target]?.Query(pattern);
        }
        public override ITuple QueryP(string target, IPattern pattern)
        {
            return this[target]?.QueryP(pattern);
        }
        public override void Put(string target, ITuple tuple)
        {
            this[target]?.Put(tuple);
        }
        public override ITuple Get(string target, params object[] pattern)
        {
            return this[target]?.Get(pattern);
        }
        public override ITuple GetP(string target, params object[] pattern)
        {
            return this[target]?.GetP(pattern);
        }
        public override ITuple Query(string target, params object[] pattern)
        {
            return this[target]?.Query(pattern);
        }
        public override ITuple QueryP(string target, params object[] pattern)
        {
            return this[target]?.QueryP(pattern);
        }
        public override void Put(string target, params object[] tuple)
        {
            this[target]?.Put(tuple);
        }


        public void StartListen()
        {
            this.listener.Start(this.OnClientConnect);
        }
        public void StopListen()
        {
            this.listener.Stop();
        }
        public void AddSpace(string identifier, ITupleSpace tuplespace)
        {
            if (!this.spaces.ContainsKey(identifier))
            {
                this.spaces.Add(identifier, tuplespace);
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private void OnClientConnect(TcpClient client)
        {
            ServerSocket socket = new ServerSocket(client);
            BasicRequest request = (BasicRequest)socket.Receive<BasicRequest>();
            this.GetProtocol(request)?.ProcessRequest(socket, request);
            socket.Close();
        }
        private ProtocolBase GetProtocol(BasicRequest request)
        {
            if (this.protocols.ContainsKey(request.Mode))
            {
                return this.protocols[request.Mode];
            }

            return null; // TODO: return response error
        }

        #endregion
    }
}
