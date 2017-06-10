using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class SpaceRepository : RepositoryBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private TcpListener listener;
        private Dictionary<string, ISpace> spaces;
        private Dictionary<ConnectionMode, ProtocolBase> protocols;
        private IEncoder encoder;
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public SpaceRepository(ConnectionMode mode, int port, string address = "", bool listenOnStart = true) : base(address, port)
        {
            this.encoder = new RepositoryEncoder();
            this.listener = new TcpListener(this.address, this.port);
            this.spaces = new Dictionary<string, ISpace>();
            this.protocols = new Dictionary<ConnectionMode, ProtocolBase>();
            mode.HasFlag(ConnectionMode.CONN).Then(() => this.protocols.Add(ConnectionMode.CONN, new ConnProtocol(this)));
            mode.HasFlag(ConnectionMode.PUSH).Then(() => this.protocols.Add(ConnectionMode.PUSH, new PushProtocol(this, string.Empty, 0)));
            mode.HasFlag(ConnectionMode.PULL).Then(() => this.protocols.Add(ConnectionMode.PULL, new PullProtocol(this)));
            if (this.protocols.Count == 0)
            {
                throw new Exception("Cannot start server without valid connectionschemes");
            }
            if (listenOnStart)
            {
                this.StartListen();
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public ISpace this[string indexer]
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
        public override ITuple Get(string target, params object[] pattern)
        {
            return this[target]?.Get(pattern);
        }
        public override ITuple GetP(string target, IPattern pattern)
        {
            return this[target]?.GetP(pattern);
        }
        public override ITuple GetP(string target, params object[] pattern)
        {
            return this[target]?.GetP(pattern);
        }
        public override IEnumerable<ITuple> GetAll(string target, IPattern pattern)
        {
            return this[target]?.GetAll(pattern);
        }
        public override IEnumerable<ITuple> GetAll(string target, params object[] pattern)
        {
            return this[target]?.GetAll(pattern);
        }
        public override ITuple Query(string target, IPattern pattern)
        {
            return this[target]?.Query(pattern);
        }
        public override ITuple Query(string target, params object[] pattern)
        {
            return this[target]?.Query(pattern);
        }
        public override ITuple QueryP(string target, IPattern pattern)
        {
            return this[target]?.QueryP(pattern);
        }
        public override ITuple QueryP(string target, params object[] pattern)
        {
            return this[target]?.QueryP(pattern);
        }
        public override IEnumerable<ITuple> QueryAll(string target, IPattern pattern)
        {
            return this[target]?.QueryAll(pattern);
        }
        public override IEnumerable<ITuple> QueryAll(string target, params object[] pattern)
        {
            return this[target]?.QueryAll(pattern);
        }
        public override void Put(string target, ITuple tuple)
        {
            this[target]?.Put(tuple);
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
        public void AddSpace(string identifier, ISpace tuplespace)
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
            Socket socket = new Socket(client, this.encoder);
            BasicRequest request = (BasicRequest)socket.Receive<BasicRequest>();
            this.GetProtocol(request)?.ProcessRequest(socket, request);
            socket.Close(); // TODO: Forcibly closing socket when done. Does it need to reply if the connectionmode is unsupported?
        }
        private ProtocolBase GetProtocol(BasicRequest request)
        {
            if (request != null && this.protocols.ContainsKey(request.Connectionmode))
            {
                return this.protocols[request.Connectionmode];
            }

            return null; // TODO: return response error
        }

        #endregion
    }
}
